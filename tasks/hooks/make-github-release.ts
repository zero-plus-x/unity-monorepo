import type { THook, TMessage, TLogReleaseType, TPackageRelease } from '@auto/core'
import type { TGithubConfig } from '@auto/github'
import fetch from 'node-fetch'
import type { TReadonly } from 'tsfn'
import { compileTagForOpenUpm, compileGithubReleaseName } from '../utils/publish-helpers'

const GITHUB_API_REPOS_URL = 'https://api.github.com/repos/'

export const makeGithubReleases = (githubConfig: TReadonly<TGithubConfig>): THook => async ({ packages, prefixes }) => {
  const compileMessages = (pkg: TReadonly<TPackageRelease>): string => {
    let result = (pkg.messages || []) as TMessage<TLogReleaseType>[]

    if (pkg.deps !== null && pkg.type !== 'initial') {
      const depNames = Object.keys(pkg.deps)
        .filter((name) => Boolean(packages.find((pkg) => pkg.name === name)?.type !== 'initial'))

      if (depNames.length > 0) {
        result = result.concat({
          type: 'dependencies',
          message: `update dependencies \`${depNames.join('`, `')}\``,
        })
      }
    }

    return result
      .map((message) => `* ${prefixes[message.type]} ${message.message}`)
      .join('\n')
  }

  for (const pkg of packages) {
    if (pkg.version === null) {
      return
    }

    await fetch(
      `${GITHUB_API_REPOS_URL}${githubConfig.username}/${githubConfig.repo}/releases`,
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `token ${githubConfig.token}`,
          'User-Agent': 'auto-tools',
        },
        body: JSON.stringify({
          tag_name: compileTagForOpenUpm(pkg),
          name: compileGithubReleaseName(pkg),
          body: compileMessages(pkg),
        }),
      }
    )
  }
}
