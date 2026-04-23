import type { Sentiment } from "@/types/chat"

export const resolveAuthorName = (name?: string) => name?.trim() || "Unknown"

export const getInitials = (name?: string) => {
  if (!name) return "U"
  const parts = name.trim().split(" ").filter(Boolean)
  if (parts.length === 0) return "U"
  return parts
    .slice(0, 2)
    .map(part => part[0]?.toUpperCase() ?? "")
    .join("")
}

export const toSentimentBadgeVariant = (sentiment?: Sentiment) => {
  if (sentiment === "positive") return "default"
  if (sentiment === "negative") return "destructive"
  return "secondary"
}
