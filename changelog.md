# ISR Data Finance Repository Changelog
Notable changes to the [Finance Repository] are documented in this file using the 
[Keep a Changelog] style. The dates specified are in coordinated universal time (UTC).

[1.0.9692]: https://www.github.com/atecoder/dn.data.finance

## [1.0.9692] - 2026-07-15
- Update packages.
- Update web assembly project.
  - Fix: Add trimmer configuration to preserve Blazor components
  - Fix: Downgrade to .NET 9, update ASP.NET Core packages to 9.0.0, enhance TrimmerRootDescriptor

## [1.0.9680] - 2026-07-03
- Web Assembly
  - Add version information to the project.
  - Apply code analysis recommendations.
  - Unify the names of the markdown files in the project.

## [1.0.9678] - 2026-07-01
- Add Web Assembly calculator.

## [1.0.9677] - 2026-06-30
- App
  - Include version.build.props.
- Maui
  - Include version.build.props.
  - Add release version properties.
  - Fix the XAML warnings in AppreciatorPage.xaml by adding x:DataType attributes to enable compiled bindings.
  - The Maui project is abandoned at this time because:
    - The Publish option is not enabled after defining publishing profiles.
    - The release is 100 times larger than a Windows Form application.
    - The release includes numerous unnecessary packages.

## [1.0.9673] - 2026-06-26
- Add reference to the BuildPackages project from the [IDE Repo].
- XUnits
  - Define financial values as Decimal.
  - Run all tests and document.
- Maui App
  - Update.

## [1.0.9666] - 2026-06-19
- SEP IRA Calculator
  - Set financial values as decimal.
  - Add .NET Maui application.

## [1.0.9664] - 2026-06-17
- SEP IRA Calculator Tests
  - Add unit tests for the appreciator calculations.
- SEP IRA Calculator App
  - Add report using the report builder.
- SEP IRA Calculator Console App
  - Output comparison report using the report builder.

## [1.0.9663] - 2026-06-16
- SEP IRA Calculator
  - Add report builder functionality.
  - Add initial values and input ranges.
- SEP IRA Calculator Console application
  - Add reports using the report builder.
  - Add initial input values and ranges.
- SEP IRA Calculator Form application
  - Add report using the report builder.

## [1.0.9659] - 2026-06-12
- SEP IRA Calculator
  - Added windows forms and XUnit tests.

## [1.0.9657] - 2026-06-10
- Created.

&copy; 2026 Integrated Scientific Resources, Inc. All rights reserved.

[Keep a Changelog]: https://keepachangelog.com/en/1.0.0/
[Finance Repository]: https://www.github.com/atecoder/dn.data.finance
[IDE Repo]: https://github.com/ATECoder/vs.ide.git
