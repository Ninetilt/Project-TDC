# Metriken

Zur Messung der Metriken haben wir v.a. **SonarQube Cloud** genutzt. Dieses Tool liefert einen breiten Überblick zu Aspekten wie Security, Maintainability, Reliability oder Test-Coverage. Die Einbindung in die aktive CI/CD-Pipeline gestaltete sich aufgrund der sehr limitierten Free-Version als Hindernis. Aus Sicherheitsgründen wurde die Sonar-Analyse daher auf einem codegleichen Fork des Repositorys eingerichtet und dort manuell ausgeführt. Als ergänzendes Tool wurde **NDepend** verwendet.

---

## Überblick Sonar-Analyse

![SonarQube Übersicht](https://github.com/user-attachments/assets/aeca5985-fce7-4630-a34b-f820fc32ee4b)

---

## Cyclomatic & Cognitive Complexity (Sonar-Analyse)

![Cyclomatic & Cognitive Complexity](https://github.com/user-attachments/assets/9e3286ea-bdc1-4cb7-bfe2-352d36a9eb0e)

---

## Ergänzende Ergebnisse NDepend

![NDepend Ergebnisse](https://github.com/user-attachments/assets/417ff557-5751-40ca-8d0c-bc9a125dd028)

---

## Zusammenfassende Erkenntnisse

### 1. Cyclomatic & Cognitive Complexity

Die **Cyclomatic Complexity** misst die Anzahl der unabhängigen Pfade in unserem Code, also wie viele verschiedene Abläufe durch Bedingungen und Schleifen möglich sind. Insgesamt liegt der Wert bei **423** (SonarQube), was bei etwa 100 Methoden einem Durchschnitt von ca. **4,2 pro Methode** entspricht. NDepend misst hier sogar einen durchschnittlichen Wert von 1.24. Da der Richtwert für gut wartbaren Code unter 10 liegt und die maximale Komplexität einzelner Methoden mit **7** (NDepend) ebenfalls im optimalen Bereich ist, zeigt dies, dass der Code übersichtlich und gut testbar ist.

Die **Cognitive Complexity** bewertet, wie schwer der Code für Entwickler zu verstehen ist, insbesondere durch Verschachtelungstiefe und Kontrollfluss. Mit einem Gesamtwert von **77** (SonarQube) liegt der Code im moderat-komplexen Bereich, verteilt auf alle Methoden. Dies spricht für eine klar strukturierte Codebasis, die Wartung und Weiterentwicklung erleichtert.

---

### 2. Technical Debt

Misst den geschätzten Aufwand, der notwendig ist, um technischen Schulden (z.B. schlechte Codequalität, offene Probleme) abzubauen und zeigt somit den Pflegezustand und die Nachhaltigkeit des Projekts.

Die technische Schuld beträgt aktuell ca. **4,69 %** des Gesamtaufwands (laut NDepend), was einem geschätzten Aufwand von etwa **3 Tagen** entspricht, um alle offenen Probleme zu beheben. Diese überschaubare Technical Debt zeigt, dass das Projekt kontinuierlich gepflegt wird. Durch gezieltes Refactoring und Priorisierung kann die technische Schuld noch weiter reduziert werden.

---

### 3. Code Duplication

Misst den Anteil doppelten Codes und hilft somit, Redundanzen zu vermeiden, welche die Wartung erschweren und Fehlerquellen erhöhen.

Die Duplikationsrate im Code liegt bei **0 %** (SonarQube), was bedeutet, dass quasi keine redundanten Codeabschnitte vorhanden sind.

---

### 4. Code-Maintainability

Es gibt **82 offene Issues** (SonarQube), überwiegend mit mittlerer Priorität, was Verbesserungspotenziale in Wartbarkeit und Refactoring zeigt.

Dennoch sind keine kritischen Sicherheitsprobleme offen.

---

### 5. Testabdeckung / Coverage

SonarCloud meldet eine Testabdeckung von **71,9 %**, was einen soliden Wert für funktionale Sicherheit und Fehlerprävention darstellt.

Die Qualitätssicherung ist weitgehend erfolgreich mit einem bestandenen Qualitätsgate.

