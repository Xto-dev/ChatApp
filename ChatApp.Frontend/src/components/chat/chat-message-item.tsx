import { SENTIMENT_CONFIG } from "@/config/chat"
import { getInitials, resolveAuthorName, toSentimentBadgeVariant } from "@/lib/chat"
import type { ChatMessage } from "@/types/chat"
import { Avatar, AvatarFallback } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Card, CardContent } from "@/components/ui/card"

type ChatMessageItemProps = {
  message: ChatMessage
}

export function ChatMessageItem({ message }: ChatMessageItemProps) {
  const sentiment = message.sentiment ?? "neutral"
  const sentimentView = SENTIMENT_CONFIG[sentiment]
  const authorName = resolveAuthorName(message.user)

  return (
    <Card
      size="sm"
      className={`gap-3 border py-3 shadow-sm ${sentimentView.toneClass}`}
    >
      <CardContent className="space-y-2 px-3">
        <div className="flex items-center gap-2">
          <Avatar size="sm">
            <AvatarFallback>{getInitials(authorName)}</AvatarFallback>
          </Avatar>
          <span className="text-sm font-semibold text-foreground">{authorName}</span>
          <Badge variant={toSentimentBadgeVariant(message.sentiment)}>
            {sentimentView.emoji} {sentimentView.label}
          </Badge>
          <span className="ml-auto text-xs text-muted-foreground">
            {new Date(message.createdAt).toLocaleTimeString()}
          </span>
        </div>
        <p className="m-0 pl-8 text-sm text-foreground/95 md:text-base">{message.text}</p>
      </CardContent>
    </Card>
  )
}
