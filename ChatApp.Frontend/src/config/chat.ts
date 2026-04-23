import type { Sentiment } from "@/types/chat"

type SentimentView = {
  emoji: string
  label: string
  toneClass: string
}

export const SENTIMENT_CONFIG: Record<Sentiment, SentimentView> = {
  positive: {
    emoji: "😊",
    label: "Positive",
    toneClass:
      "bg-emerald-500/10 border-emerald-500/30 dark:bg-emerald-500/20 dark:border-emerald-400/30",
  },
  negative: {
    emoji: "😞",
    label: "Negative",
    toneClass:
      "bg-rose-500/10 border-rose-500/30 dark:bg-rose-500/20 dark:border-rose-400/30",
  },
  neutral: {
    emoji: "😐",
    label: "Neutral",
    toneClass: "bg-background/90 border-border/60 dark:bg-muted/40 dark:border-border/70",
  },
}
