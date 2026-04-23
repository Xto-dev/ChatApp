import { useEffect, useMemo, useRef, useState } from "react"
import * as signalR from "@microsoft/signalr"

import { BACKEND_URL } from "@/config/env"
import type { ChatMessage, ConnectionStatus } from "@/types/chat"

export function useChat() {
  const [messages, setMessages] = useState<ChatMessage[]>([])
  const [isLoadingHistory, setIsLoadingHistory] = useState(true)
  const [status, setStatus] = useState<ConnectionStatus>("connecting")
  const connectionRef = useRef<signalR.HubConnection | null>(null)

  useEffect(() => {
    let isDisposed = false

    fetch(`${BACKEND_URL}/api/messages`)
      .then(r => r.json() as Promise<ChatMessage[]>)
      .then(data => {
        if (!isDisposed) setMessages(data.reverse())
      })
      .catch(console.error)
      .finally(() => {
        if (!isDisposed) setIsLoadingHistory(false)
      })

    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`${BACKEND_URL}/chatHub`)
      .withAutomaticReconnect()
      .build()

    connection.on("ReceiveMessage", (message: ChatMessage) => {
      setMessages(prev => [...prev, message])
    })

    connection.onreconnecting(() => {
      if (!isDisposed) setStatus("reconnecting")
    })

    connection.onreconnected(() => {
      if (!isDisposed) setStatus("connected")
    })

    connection.onclose(() => {
      if (!isDisposed) setStatus("disconnected")
    })

    void connection
      .start()
      .then(() => {
        if (!isDisposed) setStatus("connected")
      })
      .catch((error: unknown) => {
        const message = error instanceof Error ? error.message : String(error)
        if (!message.includes("stopped during negotiation")) {
          console.error(error)
        }
        if (!isDisposed) setStatus("disconnected")
      })

    connectionRef.current = connection

    return () => {
      isDisposed = true
      void connection.stop()
    }
  }, [])

  const sendMessage = async (text: string) => {
    const connection = connectionRef.current
    if (!connection || status !== "connected" || !text.trim()) return false
    await connection.invoke("SendMessage", { Text: text })
    return true
  }

  const canSend = useMemo(() => status === "connected", [status])

  return {
    messages,
    isLoadingHistory,
    sendMessage,
    canSend,
    status,
  }
}
