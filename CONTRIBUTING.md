# Contributing to SwissWallet

## How to be useful for the project

- Any issue labelled as [good first issue](https://github.com/swisscodernano/swisswallet/issues?q=is%3Aissue+is%3Aopen+label%3A%22good+first+issue%22) is good to start contributing to SwissWallet.
- Always focus on a specific issue in your pull request and avoid unrelated/unnecessary changes.
- Avoid working on complex problems (fees, amount decomposition, coin selection...) without extensive research on the context, either on GitHub or asking contributors.
- Consider filing a new issue or explaining in an opened issue the change that you want to make, and wait for concept ACKs to work on the implementation.
- Feel free to join our [GitHub Discussions](https://github.com/swisscodernano/swisswallet/discussions) to discuss with other contributors.

## Swiss Sovereignty Principles

SwissWallet is built on **Swiss values**: neutrality, privacy, precision, and sovereignty. When contributing, please keep in mind:

- **Swiss Coordinators**: Hardcoded and non-configurable by design
- **Privacy First**: All features must prioritize user privacy
- **Swiss Infrastructure**: Trust Swiss jurisdiction and infrastructure
- **Manual Control**: Users control updates and configuration
- **Transparency**: All code must be auditable

Read [WHY_SWISS.md](WHY_SWISS.md) to understand our philosophy.

## Automatic code clean up

**Visual Studio IDE:**

**DO** use [CodeMaid](https://www.codemaid.net/), a Visual Studio extension to automatically clean up your code on saving the file.
CodeMaid is a non-intrusive code cleanup tool.

SwissWallet's CodeMaid settings [can be found in the root of the repository](https://github.com/swisscodernano/swisswallet/blob/master/CodeMaid.config). They are automatically picked up by Visual Studio when you open the project, assuming the CodeMaid extension is installed. Unfortunately CodeMaid has no Visual Studio Code extension yet. You can check out the progress on this [under this GitHub issue](https://github.com/codecadwallader/codemaid/issues/273).

**Rider IDE:**

In Rider, you can achieve similar functionality by going to `Settings -> Tools -> Action on Save` and enabling `Reformat and Cleanup Code` and changing `Profile` to `Reformat Code`.

And also enable `Enable EditorConfig support` in `Settings -> Editor -> Code Style`.

![image](https://user-images.githubusercontent.com/16364053/159900227-627f4b67-e793-421b-836a-09660971c807.png)
![image](https://user-images.githubusercontent.com/16364053/159900956-539868b7-9fd2-44ed-9ec6-d58569bd9dbb.png)

## .editorconfig

Not only CodeMaid, but Visual Studio also enforces consistent coding style through [`.editorconfig`](https://github.com/swisscodernano/swisswallet/blob/master/.editorconfig) file.

If you are using Visual Studio Code make sure to install "C# Dev Kit" extension and add the following settings to your settings file:

```json
"editor.formatOnSave": true
```

## Technologies and scope

- [.NET SDK](https://dotnet.microsoft.com/en-us/): free, open-source, cross-platform framework for building apps. SDK version path: [global.json](https://github.com/swisscodernano/swisswallet/blob/master/global.json).
- [C#](https://dotnet.microsoft.com/en-us/languages/csharp): open-source programming language.
- Model-View-ViewModel architecture (MVVM).
- [Avalonia UI](https://www.avaloniaui.net/): framework to create cross-platform UI.
- [xUnit](https://xunit.net/): create unit tests.
- Dependencies path: [Directory.Packages.props](https://github.com/swisscodernano/swisswallet/blob/master/Directory.Packages.props).
- Developer's documentation path: [WalletWasabi.Documentation/](https://github.com/swisscodernano/swisswallet/tree/master/WalletWasabi.Documentation).

# Code conventions

## Refactoring

If you are a new contributor **DO** keep refactoring pull requests short, uncomplex and easy to verify. Experienced contributors may be more aggressive with refactoring, as they understand better the scope of the changes they are making.

**DO NOT** move or rename files in refactoring PRs. Do that in a separate PR with a description that clearly states what you are doing.

## Minimal diff policy

**DO** try to minimize the number of changes in a commit. For instance, avoid reformatting the code in a commit that addresses a bug fix. This makes code reviews easier and increases the chance of your code being merged.

**DO** separate commits in parts if they introduce non-trivial refactoring changes in code that is not directly related to the main change.

## File headers

**DO NOT** add license headers to code files. We maintain a single license file in the root of the repository.

## Warnings

**DO** build without warnings.

**DO NOT** suppress warnings with `#pragma` unless absolutely necessary. If you must suppress a warning, add a comment explaining why and reference a GitHub issue if applicable.

## Asynchronous programming

**DO** add `CancellationToken` parameters to all async methods.

**DO** propagate `CancellationToken` to all called async methods.

**DO NOT** use `.Result`, `.Wait()`, or `.GetAwaiter().GetResult()` as they can cause deadlocks. Use `await` instead.

## Dependency Injection

SwissWallet uses **manual service composition** rather than DI containers. Services are composed in `WalletWasabi.Daemon/Global.cs`.

**DO** follow existing patterns when adding new services.

**DO NOT** add DI container frameworks to the project.

## Logging

**DO** use the `Logger` service for logging.

**DO NOT** log sensitive information (keys, addresses, amounts, IP addresses).

## Tests

**DO** write unit tests for new features.

**DO** keep tests fast and isolated (no network, no file system if possible).

**DO** place tests in the `WalletWasabi.Tests` project following the existing structure.

## Pull Request workflow

1. Fork the repository
2. Create a feature branch from `master`
3. Make your changes following these guidelines
4. Run tests: `dotnet test`
5. Commit with clear, descriptive messages
6. Push to your fork
7. Open a Pull Request against `master`

## Code Review process

All submissions require review. We use GitHub pull requests for this purpose.

- Reviewers will check code quality, security, and alignment with Swiss sovereignty principles
- Address all feedback before merge
- Squash commits if requested
- Be patient - thorough reviews take time

## Questions?

- Open a [GitHub Discussion](https://github.com/swisscodernano/swisswallet/discussions)
- Check existing [Issues](https://github.com/swisscodernano/swisswallet/issues)
- Read our [Documentation](https://github.com/swisscodernano/swisswallet/tree/master/docs)

---

🇨🇭 **Thank you for contributing to Swiss Bitcoin sovereignty!**
