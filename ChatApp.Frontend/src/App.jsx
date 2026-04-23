import { useState, useEffect, useRef } from 'react'
import * as signalR from '@microsoft/signalr'
import './App.css'

const BACKEND_URL = import.meta.env.VITE_BACKEND_URL || 'http://localhost:5124'

const sentimentConfig = {
  positive: { emoji: '😊', color: '#d4edda', label: 'Positive' },
  negative: { emoji: '😞', color: '#f8d7da', label: 'Negative' },
  neutral:  { emoji: '😐', color: '#f8f9fa', label: 'Neutral'  },
}

export default function App() {
  const [messages, setMessages] = useState([])
  const [text, setText] = useState('')
  const [user, setUser] = useState('')
  const [connected, setConnected] = useState(false)
  const connectionRef = useRef(null)
  const bottomRef = useRef(null)

  useEffect(() => {
    fetch(`${BACKEND_URL}/api/messages`)
      .then(r => r.json())
      .then(data => setMessages(data.reverse()))
      .catch(console.error)

    const connection = new signalR.HubConnectionBuilder()
      .withUrl(`${BACKEND_URL}/chatHub`)
      .withAutomaticReconnect()
      .build()

    connection.on('ReceiveMessage', (message) => {
      setMessages(prev => [...prev, message])
    })

    connection.start()
      .then(() => setConnected(true))
      .catch(console.error)

    connectionRef.current = connection
    return () => connection.stop()
  }, [])

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: 'smooth' })
  }, [messages])

  const sendMessage = async () => {
    if (!text.trim() || !connected) return
    await connectionRef.current.invoke('SendMessage', {Text: text})
    setText('')
  }

  return (
    <div className="chat-container">
      <h1>💬 Real-time Chat</h1>
      
      <div className="messages">
        {messages.map(msg => {
          const s = sentimentConfig[msg.sentiment] || sentimentConfig.neutral
          return (
            <div key={msg.id} className="message" style={{ background: s.color }}>
              <div className="message-header">
                <strong>{msg.user}</strong>
                <span className="sentiment">{s.emoji} {s.label}</span>
                <span className="time">
                  {new Date(msg.createdAt).toLocaleTimeString()}
                </span>
              </div>
              <div className="message-text">{msg.text}</div>
            </div>
          )
        })}
        <div ref={bottomRef} />
      </div>

      <div className="input-area">
        <input
          placeholder="Type a message..."
          value={text}
          onChange={e => setText(e.target.value)}
          onKeyDown={e => e.key === 'Enter' && sendMessage()}
          className="input-message"
        />
        <button onClick={sendMessage} disabled={!connected}>
          {connected ? 'Send' : 'Connecting...'}
        </button>
      </div>
    </div>
  )
}