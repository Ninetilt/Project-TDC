## Übersicht der Pipeline (3 parallele Build-Jobs)

![Pipeline Übersicht](https://github.com/user-attachments/assets/f7e4b161-e9b8-41bf-a72c-8b5d129133a4)

## BuildWindows

![Build Windows](https://github.com/user-attachments/assets/9a9d82c6-ee7c-493f-9925-26628b8aef2d)

## BuildAndroid

![Build Android](https://github.com/user-attachments/assets/5e0db21a-e5f9-4ce3-8245-1ac40cd8b95e)

## BuildBackend

![Build Backend](https://github.com/user-attachments/assets/aa0d9e1e-fded-4ac8-b99b-30fd2baa4a99)

## Artefakte

![Artefakte](https://github.com/user-attachments/assets/10a18363-2a55-403b-9cfa-f742eccbfd54)


## Technische Einblicke

## Mehrplattform-Builds mit .NET MAUI Workloads  
Für das Frontend werden parallel Windows- und Android-Builds erstellt. Dabei werden spezifische .NET MAUI Workloads (u.a. maui, android, wasm-tools) dynamisch installiert, um plattformübergreifende Builds sicherzustellen.

## Manuelles Caching von NuGet-Paketen  
Durch Nutzung der `actions/cache`-Action wird der Download der NuGet-Pakete zwischengespeichert, was die Buildzeiten reduziert.

## Datenbank-Setup und Tests im Backend-Build  
Im Backend-Job wird ein SQL Server Container als Service gestartet, der automatisiert vorbereitet wird (inkl. Datenbank-Erstellung TDC.Test) und als Voraussetzung für Tests dient. Zusätzlich wird das Tool `sqlcmd` zur Datenbankverwaltung installiert.

## Artefaktverwaltung und Test-Resultate-Upload  
Nach jedem Build werden die kompilierten Artefakte (für Windows, Android und Backend) gespeichert. Die Unit-Test-Ergebnisse des Backends werden separat exportiert und hochgeladen, um eine bessere Nachverfolgbarkeit zu gewährleisten.
