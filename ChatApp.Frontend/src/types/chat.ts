export type Sentiment = "positive" | "negative" | "neutral"

export type ChatMessage = {
  id: string | number
  user?: string
  text: string
  createdAt: string
  sentiment?: Sentiment
}

export type ApiChatMessage = {
  id?: string | number
  user?: string
  text?: string
  createdAt?: string
  sentiment?: string
  sentimentLabel?: string
}

export type ConnectionStatus = "connecting" | "connected" | "reconnecting" | "disconnected"
