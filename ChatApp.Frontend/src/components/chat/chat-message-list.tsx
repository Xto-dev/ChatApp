import { useEffect, useRef } from "react"

import { ScrollArea } from "@/components/ui/scroll-area"
import { Skeleton } from "@/components/ui/skeleton"
import type { ChatMessage } from "@/types/chat"
import { ChatMessageItem } from "@/components/chat/chat-message-item"

type ChatMessageListProps = {
  messages: ChatMessage[]
  isLoading: boolean
}

export function ChatMessageList({ messages, isLoading }: ChatMessageListProps) {
  const bottomRef = useRef<HTMLDivElement | null>(null)

  useEffect(() => {
    bottomRef.current?.scrollIntoView({ behavior: "smooth" })
  }, [messages])

  return (
    <ScrollArea className="h-[58vh] min-h-[360px] rounded-3xl border border-border/60 bg-muted/20 p-3 md:p-4">
      <div className="space-y-3">
        {isLoading && (
          <>
            <Skeleton className="h-20 w-full" />
            <Skeleton className="h-24 w-11/12" />
            <Skeleton className="h-20 w-10/12" />
          </>
        )}

        {!isLoading &&
          messages.map(message => (
            <ChatMessageItem
              key={message.id ?? `${message.createdAt}-${message.text}`}
              message={message}
            />
          ))}

        {!isLoading && messages.length === 0 && (
          <div className="rounded-3xl border border-dashed border-border/80 bg-background/70 px-4 py-12 text-center text-sm text-muted-foreground">
            Сообщений пока нет. Напиши первым.
          </div>
        )}
        <div ref={bottomRef} />
      </div>
    </ScrollArea>
  )
}
