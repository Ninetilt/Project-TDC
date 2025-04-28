# **Project TDC 📝**  
*Eine kompetitive To-Do List Android-App, die es Freunden ermöglicht, sich im Abarbeiten ihrer To-Do Lists zu messen.*  

---

## **Inhaltsverzeichnis**  
1. [Über das Projekt](#über-das-projekt)  
2. [Vision](#vision)  
3. [Technologien](#technologien)  
4. [Team](#team)  
5. [Scrum-Setup](#scrum-setup)  
6. [Funktionale Anforderungen](#funktionale-anforderungen)  
7. [Nichtfunktionale Anforderungen](#nichtfunktionale-anforderungen)  
8. [UI-Mockup](#ui-mockup)  
9. [Klassendiagramm](#klassendiagramm)  
10. [Projektaufbau](#projektaufbau)  
11. [Git-Workflow](#git-workflow)  
12. [Sprint-Zusammenfassungen](#sprint-zusammenfassungen)  

---

## **Über das Projekt**  
**Project TDC** verwandelt To-Do-Listen in einen interaktiven Wettbewerb.  
- **Features:** Punkte für erledigte Aufgaben, ein personalisierbarer Avatar und eine Freundesliste.  
- **Ziel:** Steigere deinen Avatar und trete gegen andere an, um deinen Fortschritt sichtbar zu machen.  



## **Vision**  
Unsere Vision ist es, eine **benutzerfreundliche und stabile** App zu entwickeln, die:  
- **Spaß** in den Alltag bringt.  
- Mit einer **intuitiven UI** und **starker Performance** glänzt.  
- Agile Methoden und moderne Technologien nutzt, um kontinuierliche Verbesserungen zu garantieren.  



## **Technologien**  
- **Programmiersprache:** C#  
- **Framework:** .NET MAUI (Cross-Platform)  
- **IDE:** Visual Studio  
- **Versionsverwaltung:** Git  
- **CI/CD:** GitHub Actions 
- **Tests:** Unit- und Integrationstests (geplant ab Sem 4)  



## **Team**  
| Name         | Rolle                  |  
|--------------|------------------------|  
| **Hannah**   | Product Owner, Entwickler |  
| **Christian**| Scrum-Master, Entwickler |  
| **Julian**   | Blog-Master, Entwickler  |  
| **Leo**      | Entwickler              |  
| **Nico**     | Entwickler              |  



## **Scrum-Setup**  
- **Sprint-Länge:** 2 Wochen  
- **Meetings:**  
  - Weekly: Montag, 20–21:30 Uhr  
  - Retrospektiven: alle 2 Wochen  
  - Dailys: bei Bedarf  
- **Story Points:** 1 SP = 1 Stunde (2er Potenz: 2, 4, 8, 16)  
- **Tools:** Jira (Backlog, Scrum-Board)  



## **Funktionale Anforderungen**  
1. **Benutzerverwaltung:**  
   - Registrierung, Anmeldung, Profilbearbeitung, Passwort zurücksetzen.  
2. **To-Do-Listen:**  
   - Erstellen, Bearbeiten, Abschließen und Löschen von Aufgaben.  
3. **Freunde:**  
   - Freunde hinzufügen, verwalten und Ranglisten anzeigen.  



## **Nichtfunktionale Anforderungen**  
- **Performance:** Ladezeiten unter 1-2 Sekunden.  
- **Usability:** Einfache Bedienung ohne lange Einarbeitungszeit.  
- **Stabilität/Zuverlässigkeit:** Weniger als 1% Abstürze pro 1000 Aktionen.  
- **Sicherheit:** Sicheres Konto und Passwortzurücksetzung.
- **Interoperabilität:** Verwendung von API-integration.



## **UI-Mockup**  
Das Design ist darauf ausgelegt, Informationen klar darzustellen:  
- **Hauptbildschirm:** Aktive Listen und Fortschrittspunkte.  
- **Avatar:** Über das Profil erreichbar.  
- **Navigation:** Schnellzugriff auf Freundesliste und Einstellungen.  

*(UI-Skizzen sind in `/docs` verfügbar.)*  



## **Klassendiagramm**  
Das Klassendiagramm beschreibt die wichtigsten Komponenten und ihre Beziehungen.  
- **Designprinzipien:** SOLID (Single Responsibility, Open/Closed).  
- **Dateipfad:** `/docs/klassendiagramm.pdf`  

---
## **Projektaufbau**  
- **Codebasis:** Visual Studio (.NET MAUI)  
- **Git:** Strukturierte Branches für Übersichtlichkeit.  
- **Android-Simulation:** Debugging direkt auf Mobilgeräten oder Emulator.  



## **Git-Workflow**  
### **Branch-Struktur**  
- `main`: Stable Release  
- `dev`: Aktuelle Entwicklungsbasis  
- `feature/[TDC-Nummer]`: Neue Features  
- `bug/[TDC-Nummer]`: Fehlerbehebungen  
- `architecture/[Änderung]`: Architekturänderungen  

### **Standardanweisungen**
#### **Standart 'get/set'-Methoden**
```C#
private int beispielInt { get; set; }
```
#### **Branch erstellen:** 
>Füge folgende befehle in VS im Workspace aus: `/Project-TDC/code/TDC/TDC`
```git
git checkout dev
git pull  
git checkout -b feature/<branch-name>
// Code ändern und anpassen
git add .
git commit -m [commitMessage]
git push
```
#### **Pull Request erstellen:**
>Am besten über GitHub im Browser
1. Navigiere zum reiter `Pull Requests`
2. Erstelle `New Pull request`
3. Wähle deinen Basisbranch aus, in welchen du mergen willst (meist `dev`)
4. Wähle den Branch, den du in den ausgewählten Basisbranch mergen möchtest (meist dein `feature/TDC-X` branch auf welchem du gearbeitet hast
5. Erstelle den Pull Request und warte auf ein Review


Hinweis:  feature -> dev :: squash-merge
          dev -> main :: rebase-merge
---


---

