import { Badge } from "@/components/ui/badge"
import { CardDescription, CardTitle } from "@/components/ui/card"
import { Separator } from "@/components/ui/separator"
import { Switch } from "@/components/ui/switch"
import type { ConnectionStatus } from "@/types/chat"

type ChatHeaderProps = {
  status: ConnectionStatus
  isDark: boolean
  onThemeChange: (checked: boolean) => void
}

const statusLabelMap: Record<ConnectionStatus, string> = {
  connecting: "Connecting...",
  connected: "Online",
  reconnecting: "Reconnecting...",
  disconnected: "Offline",
}

const statusBadgeVariantMap: Record<ConnectionStatus, "default" | "secondary" | "destructive"> = {
  connected: "default",
  connecting: "secondary",
  reconnecting: "secondary",
  disconnected: "destructive",
}

export function ChatHeader({ status, isDark, onThemeChange }: ChatHeaderProps) {
  return (
    <>
      <div className="flex flex-wrap items-center justify-between gap-4">
        <div>
          <CardTitle className="text-2xl md:text-3xl">Real-time Chat</CardTitle>
          <CardDescription>SignalR + sentiment feed</CardDescription>
        </div>
        <div className="flex items-center gap-3">
          <Badge variant={statusBadgeVariantMap[status]}>{statusLabelMap[status]}</Badge>
          <div className="flex items-center gap-2 rounded-full border border-border px-3 py-1.5">
            <span className="text-xs text-muted-foreground">Dark</span>
            <Switch checked={isDark} onCheckedChange={onThemeChange} />
          </div>
        </div>
      </div>
      <Separator />
    </>
  )
}
