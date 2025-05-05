# arc42 Template für Project TDC

## 1. Einführung und Ziele

### 1.1 Aufgabenstellung
Project TDC ist eine To-Do-App mit spielerischem Wettbewerbselement. Nutzer sammeln Punkte für erledigte Aufgaben und vergleichen sich mit Freunden, um Motivation und Produktivität zu steigern.

### 1.2 Qualitätsziele
- **Benutzbarkeit**: Die App soll intuitiv und leicht verständlich bedienbar sein.
- **Performance**: Schnelle Ladezeiten für Listen, Aufgaben oder Profiländerungen.
- **Zuverlässigkeit**: Das System muss stabil laufen.
- **Spaßfaktor / Gamification**: Das Belohnungs- und Wettbewerbssystem soll Nutzer aktivieren und zur Nutzung anregen.
- **Datensicherheit**: Sichere Authentifizierung, Passwort-Reset via E-Mail
- **Interoperabilität**: API-Integration, z.B. Freundesverwaltung

### 1.3 Stakeholder
- Endnutzer (z. B. Schüler, Studierende, Erwachsene)
- Projektteam
- Betreuender Dozent als Auftraggeber

---

## 2. Randbedingungen

### 2.1 Technische Randbedingungen
- Programmiersprache: C#
- Plattform: .NET MAUI (Cross-Plattform)
- Architektur: MVVM
- Entwicklungsumgebung: Visual Studio
- Source Control: GitHub

### 2.2 Organisatorische Randbedingungen
- Entwicklungszeitraum: Semesterprojekt (2 Semester)
- Agile Entwicklung (Scrum): Sprintlänge 2 Wochen
- Kommunikation in Präsenz oder über Discord

### 2.3 Konventionen
- Getter & Setter
- Feature-Branches
- Code Review
- Tests

---

## 3. Kontextabgrenzung

### Fachlicher Kontext

| Kommunikations-teilnehmer | Beschreibung | Eingaben | Ausgaben |
|---------------------------|--------------|----------|----------|
| Nutzer:in | Verwaltet persönliche Aufgaben und interagiert mit Freund:innen | Neue Aufgaben/Listen, Aufgaben abhaken, Freunde einladen, Kämpfe starten | Listen, Fortschritt/Level, Kampfresultate, Freundesstatus |

### Technischer Kontext

| Kommunikation | Technologie/Protokoll | Beschreibung |
|---------------|-----------------------|--------------|
| Frontend → Backend | JSON über HTTPS | API-Aufrufe: Aufgaben, Kämpfe, Nutzer |
| Backend → Datenbank | SQL | Daten speichern & abfragen |

---

## 4. Lösungsstrategie

### Technologien
- C# mit .NET MAUI
- Datenbank: Lokale Persistenz für Aufgaben und Nutzerdaten
- Git & GitHub: Versionskontrolle, Projektkoordination

### Architekturansatz
- MVVM: Trennung von UI, Logik und Daten
- Modulstruktur: Authentifizierung, To-Do-Verwaltung, Freunde, Belohnungssystem
- Repositories & Services: Klare Zuständigkeiten für Datenzugriff

### Qualitätsorientierung
- Benutzbarkeit: Intuitive UI, basierend auf Mockups
- Zuverlässigkeit: Offlinefähig, schnelle Reaktion, stabile Datenhaltung
- Sicherheit: Passwort-Reset via E-Mail-Link, sichere Authentifizierung

### Organisation
- Agiles Vorgehen: Sprints, GitHub-Boards, klare Rollen
- Kommunikation: regelmäßige Abstimmung im Team

---

## 5. Bausteinsicht
![image](https://github.com/user-attachments/assets/f1ad2f5b-2644-4501-a27f-766b496ee6de)


---

## 6. Laufzeitsicht
![image](https://github.com/user-attachments/assets/bf71fe87-c0ee-4507-bffa-494222219b14)


---

## 7. Verteilungssicht
*(tbd)*

---

## 8. Querschnittliche Konzepte
*(tbd)*

---

## 9. Architekturentscheidungen

### SOA-Architektur (ADR #1)
Um Modularität und Skalierbarkeit zu ermöglichen, wurde ein serviceorientierter Architekturstil gewählt. Microservices erlauben eine einfache Erweiterung und klare Trennung der Funktionen.

### Asynchrone Kommunikation (ADR #2)
Zur Verbesserung der Performance setzt die App auf asynchrone API-Aufrufe. So bleibt die Nutzeroberfläche reaktionsschnell, auch wenn serverseitige Prozesse noch laufen.

### RESTful APIs (ADR #3)
Für die Kommunikation zwischen den Microservices wurde REST eingesetzt – eine einfache, weit verbreitete und plattformunabhängige Lösung.

---

## 10. Qualitätsanforderungen

![image](https://github.com/user-attachments/assets/a24b6717-15a0-431f-a161-0b164ad4e270)


### Zuverlässigkeit (S01)
Die App soll auch bei 1000 gleichzeitigen Benutzeraktionen stabil bleiben. Geplant ist ein Retry-Mechanismus zur Fehlerbehandlung (z. B. mit Polly).

### Leistungsfähigkeit (S02)
Neue To-Do-Listen sollen innerhalb von 2 Sekunden verfügbar sein. Dafür wird lokales Caching kombiniert mit asynchronen API-Aufrufen verwendet.

### Benutzerfreundlichkeit (S03)
Registrierung und Login sollen schnell und intuitiv erfolgen (Registrierung < 30 Sek., Login < 10 Sek.). Eine klare UI mit Instant-Feedback ist vorgesehen.

### Sicherheit (S04)
Passwort-Zurücksetzen erfolgt über einen sicheren, zeitlich begrenzten Link per E-Mail. Die Umsetzung basiert z. B. auf ASP.NET Core Identity.

### Interoperabilität (S05)
Die Freundesliste wird über eine RESTful API verwaltet. Ein Freund kann hinzugefügt werden, der innerhalb von 2 Sekunden eine Benachrichtigung erhält.

---

## 11. Risiken und technische Schulden

| Risiko-ID       | Beschreibung | Wahrscheinlichkeit (1 - 3) | Schadenshöhe (1 - 3) | Risiko-Score | Minimierungs-Strategie | Indikatoren | Notfallplan | Status | Verantwortlicher | Datum der letzten Aktualisierung |
|------------------|--------------|-----------------------------|------------------------|---------------|--------------------------|--------------|--------------|--------|-------------------|-------------------------------|
| R-T001 | MAUI-Framework funktioniert nicht stabil auf allen Plattformen | 2 | 3 | 6 | Vorab-Tests auf Zielplattformen (Android, Windows) | Plattformabhängige Bugs | UI fallback, ggf. Native ersetzen | Offen | Dev-Team | 2025-04-22 |
| R-T002 | Fehlerhafte Multiplayer-Synchronisation | 1 | 2 | 2 | Implementierung von Transaktionen/Locks | Fehlende/Fehlerhafte Task-Sync | Temporäre Deaktivierung von gemeinsamen Listen | Offen | Backend Dev | 2025-04-22 |
| R-T003 | UI-Design-Umsetzung mit .NET-MAUI wird schwierig | 2 | 2 | 4 | UI vereinfachen | Fehlerhafte Anzeige | Verinfachtes Basis-UI | Offen | UI-Dev | 2025-04-22 |
| R-T004 | Keine automatisierten Tests vorhanden | 2 | 2 | 4 | Einführung von Unit/UI Tests in CI | Häufige Bugs nach Änderungen | Manuelles Testen + Hotfix-Plan | Offen | Dev Team | 2025-04-22 |
| R-O001 | Anforderungen ändern sich spät im Projekt | 2 | 2 | 4 | Dokumentation, klare Kommunikation | Widersprüchliche/Fehlende Tickets | Repriorisierung | Offen | Product Owner | 2025-04-22 |
---

## 12. Glossar

List = To-Do-Liste
List Item = Task auf einer To-Do-Liste
List Member = Teilnehmer an einer To-Do-Liste
TDC = "To-Do-Competition" 
XP = Erfahrungspunkte
