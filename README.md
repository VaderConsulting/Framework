# Framework

VB.NET 3.5 working copy of `Utility.sln` (VS 2008). `Utility` (assembly Utility 1.7.13.0, Stratatel Ltd) is a class library for CADS XML config, AD/LDAP (`Directory`, `ADUser`), logging, WCF `Host`/`IService`, and `frmLogin`. `SettingsLoader` in `TestHarness` is the WinForms Settings Loader that calls `Functions.DoLogon` and lists config keys; `InDevelopment` adds CADS3 LDAP import (`CADS3Import`), LDAP helpers, logging, and a Licensing stub. Open `Utility.sln`. This is a historical working copy from Dave Robinson / Vader Consulting (Stratatel-era CADS helpers).

**Source last updated:** 2019-10-25  
**Language:** VB.NET  
**Target:** v3.5  
**Output:** Library, WinExe

## What it is

VB.NET 3.5 working copy of `Utility.sln` (VS 2008). `Utility` (assembly Utility 1.7.13.0, Stratatel Ltd) is a class library for CADS XML config, AD/LDAP (`Directory`, `ADUser`), logging, WCF `Host`/`IService`, and `frmLogin`. `SettingsLoader` in `TestHarness` is the WinForms Settings Loader that calls `Functions.DoLogon` and lists config keys; `InDevelopment` adds CADS3 LDAP import (`CADS3Import`), LDAP helpers, logging, and a Licensing stub. Open `Utility.sln`. This is a historical working copy from Dave Robinson / Vader Consulting (Stratatel-era CADS helpers).

## Solution structure

| Project | Language | Path |
|---------|----------|------|
| `SettingsLoader` | VB.NET | `TestHarness/SettingsLoader.vbproj` |
| `TestHarness` | VB.NET | `TestHarness/TestHarness.vbproj` |
| `InDevelopment` | VB.NET | `InDevelopment/InDevelopment.vbproj` |
| `Utility` | VB.NET | `Utility/Utility.vbproj` |
| `Utility` | VB.NET | `Utility/Backup/Utility.vbproj` |
| `TestHarness` | VB.NET | `Utility/Backup/TestHarness/TestHarness.vbproj` |
| `InDevelopment` | VB.NET | `Utility/Backup/InDevelopment/InDevelopment.vbproj` |

## How to open

Open `Utility.sln` in Visual Studio.

## Requirements

- Visual Studio 2008, .NET Framework 3.5

## Attribution and provenance

Working copy from my Historical Dev folder `Framework`.
- **Assembly company:** Microsoft, Stratatel, Stratatel Ltd
- **Assembly copyright:** Copyright © Microsoft 2008, Copyright © Stratatel 2008, Copyright © Stratatel Ltd 2008

## License

MIT. See `LICENSE`.
