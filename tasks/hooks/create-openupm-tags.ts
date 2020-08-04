import type { THook } from '@auto/core'
import execa from 'execa'
import { compileTagForOpenUpm } from '../utils/publish-helpers'

export const createOpenUpmTags: THook = async ({ packages }) => {
  for (const pkg of packages) {
    if (pkg.version === null) {
      continue
    }

    const tagText = compileTagForOpenUpm(pkg)

    await execa(
      'git',
      [
        'tag',
        '-a',
        tagText,
        '-m',
        tagText,
      ],
      {
        stdout: 'ignore',
        stderr: 'inherit',
      }
    )
  }
}
