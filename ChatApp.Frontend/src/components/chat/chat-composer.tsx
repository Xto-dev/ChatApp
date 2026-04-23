import { useState } from "react"

import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"

type ChatComposerProps = {
  canSend: boolean
  onSend: (text: string) => Promise<boolean>
}

export function ChatComposer({ canSend, onSend }: ChatComposerProps) {
  const [text, setText] = useState("")

  const handleSend = async () => {
    const messageSent = await onSend(text)
    if (messageSent) setText("")
  }

  return (
    <div className="flex w-full items-center gap-2">
      <Input
        placeholder="Type a message..."
        value={text}
        onChange={e => setText(e.target.value)}
        onKeyDown={e => e.key === "Enter" && void handleSend()}
      />
      <Button onClick={() => void handleSend()} disabled={!canSend || !text.trim()} size="lg">
        Send
      </Button>
    </div>
  )
}
