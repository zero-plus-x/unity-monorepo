const sleep = (timeout: number) => new Promise((resolve) => setTimeout(resolve, timeout))

export type TWaitForPortConfig = {
  path?: string,
  timeoutMs?: number,
  logMessage?: (message: string) => void,
}

export const waitForPort = async (port: number, { path = '', timeoutMs = 5000, logMessage = () => {} }: TWaitForPortConfig = {}) => {
  const { default: fetch } = await import('node-fetch')
  const isResponding = async (url: string) => {
    try {
      logMessage(`fetching URL: ${url}`)
      await fetch(url, { timeout: 500 })

      return true
    } catch {
      return false
    }
  }

  const url = `http://localhost:${port}/${path}`
  const maxTries = Math.round(Math.max(timeoutMs, 1000) / 500)
  let numTries = 0

  while (numTries++ < maxTries && !(await isResponding(url))) {
    await sleep(500)
  }
}
