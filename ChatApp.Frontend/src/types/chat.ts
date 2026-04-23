export type Sentiment = "positive" | "negative" | "neutral"

export type ChatMessage = {
  id: string | number
  user?: string
  text: string
  createdAt: string
  sentiment?: Sentiment
}

export type ConnectionStatus = "connecting" | "connected" | "reconnecting" | "disconnected"
