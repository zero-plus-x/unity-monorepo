import path from 'path'
import type { TPackageRelease } from '@auto/core'
import type { TGithubConfig } from '@auto/github'
import type { TSlackConfig } from '@auto/slack'
import type { TTelegramConfig } from '@auto/telegram'
import type { TReadonly } from 'tsfn'
import { isValidString } from './is-valid-string'

export const getPublishDir = (packageDir: string) => {
  const relativeDir = path.relative(path.resolve('Packages'), packageDir)

  return path.resolve('Publish', relativeDir)
}

export type TAutoConfig = {
  shouldMakeGitTags: boolean,
  shouldMakeGitHubReleases: boolean,
  shouldSendSlackMessage: boolean,
  shouldSendTelegramMessage: boolean,
  shouldWriteChangelogFiles: boolean,
}

export const getAutoConfig = async (): Promise<TAutoConfig> => {
  const { start } = await import(path.resolve('package.json'))
  const auto = start?.auto ?? {}

  return {
    shouldMakeGitHubReleases: Boolean(auto.shouldMakeGitHubReleases),
    shouldMakeGitTags: Boolean(auto.shouldMakeGitTags),
    shouldSendSlackMessage: Boolean(auto.shouldSendSlackMessage),
    shouldSendTelegramMessage: Boolean(auto.shouldSendTelegramMessage),
    shouldWriteChangelogFiles: Boolean(auto.shouldWriteChangelogFiles),
  }
}

export const getSlackConfig = (): TSlackConfig => {
  const token = process.env.AUTO_SLACK_TOKEN
  const channel = process.env.AUTO_SLACK_CHANNEL
  const username = process.env.AUTO_SLACK_USERNAME
  const iconEmoji = process.env.AUTO_SLACK_ICON_EMOJI

  if (!isValidString(token)) {
    throw new Error('"AUTO_SLACK_TOKEN" env is not set')
  }

  if (!isValidString(channel)) {
    throw new Error('"AUTO_SLACK_CHANNEL" env is not set')
  }

  if (!isValidString(username)) {
    throw new Error('"AUTO_SLACK_USERNAME" env is not set')
  }

  if (!isValidString(iconEmoji)) {
    throw new Error('"AUTO_SLACK_ICON_EMOJI" env is not set')
  }

  const initial = process.env.AUTO_SLACK_COLOR_INITIAL
  const major = process.env.AUTO_SLACK_COLOR_MAJOR
  const minor = process.env.AUTO_SLACK_COLOR_MINOR
  const patch = process.env.AUTO_SLACK_COLOR_PATCH

  if (!isValidString(initial)) {
    throw new Error('"AUTO_SLACK_COLOR_INITIAL" env is not set')
  }

  if (!isValidString(major)) {
    throw new Error('"AUTO_SLACK_COLOR_MAJOR" env is not set')
  }

  if (!isValidString(minor)) {
    throw new Error('"AUTO_SLACK_COLOR_MINOR" env is not set')
  }

  if (!isValidString(patch)) {
    throw new Error('"AUTO_SLACK_COLOR_PATCH" env is not set')
  }

  return {
    token,
    channel,
    username,
    iconEmoji,
    colors: {
      initial,
      major,
      minor,
      patch,
    },
  }
}

export const getGithubConfig = (): TGithubConfig => {
  const token = process.env.AUTO_GITHUB_TOKEN
  const username = process.env.AUTO_GITHUB_USERNAME
  const repo = process.env.AUTO_GITHUB_REPO

  if (!isValidString(token)) {
    throw new Error('"AUTO_GITHUB_TOKEN" env is not set')
  }

  if (!isValidString(username)) {
    throw new Error('"AUTO_GITHUB_USERNAME" env is not set')
  }

  if (!isValidString(repo)) {
    throw new Error('"AUTO_GITHUB_REPO" env is not set')
  }

  return {
    token,
    username,
    repo,
  }
}

export const getTelegramConfig = (): TTelegramConfig => {
  const token = process.env.AUTO_TELEGRAM_TOKEN
  const chatId = process.env.AUTO_TELEGRAM_CHAT_ID

  if (!isValidString(token)) {
    throw new Error('"AUTO_TELEGRAM_TOKEN" env is not set')
  }

  if (!isValidString(chatId)) {
    throw new Error('"AUTO_TELEGRAM_CHAT_ID" env is not set')
  }

  return {
    token,
    chatId,
  }
}

const getPackageNamePart = (packageFullName: string) => {
  const parts = packageFullName.split('.')

  return parts[parts.length - 1]
}

export const compileTagForOpenUpm = (pkg: TReadonly<TPackageRelease>) => {
  return `${getPackageNamePart(pkg.name)}_${pkg.version}`
}

export const compileGithubReleaseName = (pkg: TReadonly<TPackageRelease>) => {
  return `${getPackageNamePart(pkg.name)} v${pkg.version}`
}
