import { isValidString } from './is-valid-string'

export const getStartTasksFile = async (): Promise<string> => {
  const path = await import('path')
  const { start } = await import(path.resolve('package.json'))
  const file = start?.file

  if (isValidString(file)) {
    return path.resolve(file)
  }

  return path.resolve('tasks', 'index.ts')
}
