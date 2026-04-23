import { useEffect, useState } from "react"

const THEME_KEY = "theme"

export function useTheme() {
  const [isDark, setIsDark] = useState(false)

  useEffect(() => {
    const savedTheme = localStorage.getItem(THEME_KEY)
    const prefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches
    const useDark = savedTheme ? savedTheme === "dark" : prefersDark
    document.documentElement.classList.toggle("dark", useDark)
    setIsDark(useDark)
  }, [])

  const toggleTheme = (checked: boolean) => {
    setIsDark(checked)
    document.documentElement.classList.toggle("dark", checked)
    localStorage.setItem(THEME_KEY, checked ? "dark" : "light")
  }

  return { isDark, toggleTheme }
}
