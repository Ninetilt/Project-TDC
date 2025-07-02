# Testbericht Projekt TDC

**Version 1.0**  
**01.07.2025**

---

## Inhaltsverzeichnis

1. [Einleitung](#1-einleitung)  
   1.1 [Zu testendes System](#11-zu-testendes-system)  
   1.2 [Ziel des Tests](#12-ziel-des-tests)  
   1.3 [Testumfang und Abgrenzung](#13-testumfang-und-abgrenzung)  
2. [Teststrategie](#2-teststrategie)  
   2.1 [Vorgehen](#21-vorgehen)  
   2.2 [Testarten](#22-testarten)  
   2.3 [Tools](#23-tools)  
3. [Testplan](#3-testplan)  
4. [Testfälle](#4-testfälle)  
5. [Testergebnisse](#5-testergebnisse)  
6. [Metriken](#6-metriken)  
7. [Empfehlungen](#7-empfehlungen)  
8. [Schlussfolgerungen](#8-schlussfolgerungen)  

---

## 1. Einleitung

### 1.1 Zu testendes System

Das zu testende System ist eine plattformübergreifende mobile Anwendung zur Verwaltung von To-Do-Listen ...  
(Frontend: .NET MAUI, Backend: ASP.NET Core, DB: SQL Server)

### 1.2 Ziel des Tests

Funktionale Überprüfung der Features: Aufgaben anlegen, bearbeiten, löschen, teilen und Synchronisation.

### 1.3 Testumfang und Abgrenzung

Testfokus auf Kernfunktionen; keine Performance- oder Sicherheitstests; keine Lokalisierung; keine alten OS-Versionen.

---

## 2. Teststrategie

### 2.1 Vorgehen

- Testgetriebene Entwicklung im Backend (TDD)
- Manuelle Tests im Frontend nach Implementierung
- Keine systematische Testfalldokumentation – stattdessen dokumentiert im Code, Pull Requests und Repository

### 2.2 Testarten

- Unit-Tests (Backend)  
- Manuelle Tests (Frontend)  
- Explorative Tests  

### 2.3 Tools

- NUnit  
- FluentAssertions  
- NSubstitute  
- GitHub Actions (CI/CD)

---

## 3. Testplan

- Iterative, kontinuierliche Tests
- Verantwortlichkeit bei den jeweiligen Entwicklern
- Kein Staging-System, sondern lokale Testumgebung
- CI über GitHub Actions

**Tabelle 1: Übersicht der getesteten Features**

| Feature | Testzeitpunkt | Getestet von |
|--------|----------------|---------------|
| Erstellung, Bearbeitung und Speicherung von Listen | 14.11.2024, 13.05.2025 | Hannah Moog, Leo Ruhnau |
| Sämtliche Backend-Funktionalitäten | 27.04.2025 | Hannah Moog |
| Login für User | 15.04.2025, 13.05.2025 | Hannah Moog, Leo Ruhnau |
| Freunde hinzufügen und entfernen | 26.05.2025 | Christian Rudisile, Hannah Moog |
| Listenabschluss und Ergebnisse | 27.05.2025 | Hannah Moog, Leo Ruhnau |
| Freunde zu Listen hinzufügen/entfernen | 13.06.2025 | Hannah Moog, Leo Ruhnau, Nico Lanz |
| Abschlusstest für finale Demo | 15.06.2025 | Hannah Moog |

---

## 4. Testfälle

**Tabelle 2: Übersicht der wichtigsten Testfälle**

| Feature | Testfall | Bestanden | Fehler | Schweregrad | Maßnahme |
|--------|----------|-----------|--------|-------------|----------|
| Listen-Erstellung | Leerer Titel | Nein | Null Reference Exception | Hoch | Validierung beim Speichern |
| Listen-Erstellung | Gültiger Titel | Ja | - | - | - |
| Listen löschen | Bestehende Liste löschen | Ja | - | - | - |
| Listen-Bearbeitung | Item-Status ändern | Nein | Falscher Status | Mittel | Gezielte Speicherung |
| User-Login | Falsches Passwort | Nein | Keine Reaktion | Niedrig | bool-Login-Check |
| Freunde hinzufügen | Ungültiger Username | Nein | SQL Exception | Hoch | Validierung im Frontend |
| Listenabschluss | Reward-Nachricht wiederholt | Nein | Wiederholung | Niedrig | "Ok"-Button mit Datenbanklöschung |
| ... | ... | ... | ... | ... | ... |

_(vollständige Liste siehe Originaldokument)_

---

## 5. Testergebnisse

- Keine kritischen Blocker
- Fehler wie NullReferenceException und SQL-Fehler wurden behoben
- GitHub Actions half bei frühzeitiger Erkennung im Backend
- Anwendung funktional stabil und robust

---

## 6. Metriken

**Tabelle 3: Unit-Tests im Backend**

| Komponente | Schicht | Tests | Abdeckung |
|------------|--------|--------|------------|
| Account Handler | Domain | 34 | 98% |
| List Handler | Domain | 34 | 95% |
| ... | ... | ... | ... |

**Ø Testabdeckung Backend:** **98,04%**

**Tabelle 4: Manuelle Tests im Frontend**

| Feature | Anzahl Tests | Abdeckung |
|---------|--------------|------------|
| Listenverwaltung | 50 | 85% |
| Freunde verwalten | 15 | 95% |
| Profil | 5 | 100% |

**Ø Testabdeckung Frontend:** **85,25%**

---

## 7. Empfehlungen

- Manuelles Testen beibehalten
- Frontend-Tests automatisieren
- Strukturierte Testdokumentation einführen
- Fehler-Schweregrade standardisieren
- Frontend/Backend-Kopplung reduzieren
- CI/CD erweitern um Linting, UI-Tests etc.

---

## 8. Schlussfolgerungen

- Anwendung ist funktional stabil und gut getestet
- GitHub Actions waren effektiv für CI
- Alle bekannten Fehler wurden behoben
- Verbesserungsmöglichkeiten erkannt (Frontend-Testautomatisierung, Dokumentation)
- Aus Testsicht bereit für produktive Nutzung oder Integration

---
