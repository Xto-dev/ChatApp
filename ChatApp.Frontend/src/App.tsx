/// <reference types="vite/client" />
import { ChatComposer } from "@/components/chat/chat-composer"
import { ChatHeader } from "@/components/chat/chat-header"
import { ChatMessageList } from "@/components/chat/chat-message-list"
import { Card, CardContent, CardFooter, CardHeader } from "@/components/ui/card"
import { useChat } from "@/hooks/use-chat"
import { useTheme } from "@/hooks/use-theme"

export default function App() {
  const { isDark, toggleTheme } = useTheme()
  const { messages, isLoadingHistory, sendMessage, canSend, status } = useChat()

  return (
    <main className="min-h-svh bg-gradient-to-b from-background via-muted/20 to-background px-4 py-8 md:px-8">
      <Card className="mx-auto w-full max-w-5xl border border-border/70 bg-card/95 text-left shadow-xl backdrop-blur-sm">
        <CardHeader className="gap-4">
          <ChatHeader status={status} isDark={isDark} onThemeChange={toggleTheme} />
        </CardHeader>

        <CardContent>
          <ChatMessageList messages={messages} isLoading={isLoadingHistory} />
        </CardContent>

        <CardFooter className="border-t pt-4">
          <ChatComposer canSend={canSend} onSend={sendMessage} />
        </CardFooter>
      </Card>
    </main>
  )
}