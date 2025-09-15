# PlayVault

**PlayVault** è un sito realizzato con **.NET 8.0**, progetto del corso TPSIT.  
L’obiettivo è costruire una piattaforma web moderna che permette di **gestire contenuti multimediali** (video, musica, giochi, etc.), con funzionalità di base come autenticazione, upload, visualizzazione e gestione degli utenti.

---

## 📂 Struttura del Progetto

Ecco i file e le cartelle principali:

| Elemento | Descrizione |
|---|---|
| `PlayVault.sln` | Soluzione .NET che include tutti i progetti del sito. |
| `docker-compose.yml` / `docker-compose.override.yml` | File per orchestrare i servizi tramite Docker, definire i container necessari. |
| `.dockerignore` / `.gitignore` | Configurazioni per ignorare file inutili in Docker o Git. |
| `launchSettings.json` | Impostazioni di avvio per l’ambiente di sviluppo locale. |
| Cartelle del progetto web | Contengono il frontend (HTML, CSS, JavaScript) e il backend (C#) integrati. |

---

## 🛠 Tecnologie Utilizzate

- **.NET 8.0** come framework backend.  
- **C#** per la logica server.  
- HTML, CSS, JavaScript per il frontend.  
- File Docker e Docker Compose per containerizzazione e sviluppo in ambienti isolati.  

---

## 🚀 Come Avviare il Progetto Localmente

Ecco i passi per far partire PlayVault in locale:

1. Clona la repository:
   ```bash
   git clone https://github.com/Digizzz/PlayVault.git
   ```
2. Entra nella cartella del progetto:
   ```bash
   cd PlayVault
   ```
3. Assicurati di avere installati:
   - .NET 8.0
   - Docker & Docker Compose (se vuoi usare i container)
4. Se vuoi usare Docker:
   ```bash
   docker-compose up --build
   ```
   Questo comando costruisce i container e avvia l’app completa.
5. Altrimenti, puoi avviare direttamente il progetto nella tua IDE (Visual Studio / VS Code ecc.) usando il comando:
   ```bash
   dotnet run --project <path-to-web-project>
   ```
6. Apri il browser e vai all’indirizzo locale (di solito `https://localhost:port` o `http://localhost:port`) per vedere l’app in funzione.

---

## 🔐 Autenticazione e Sicurezza

- Il sistema include funzionalità base di autenticazione (login / logout / registrazione).  
- (Opzionale) Gestione dei ruoli utenti: amministratore vs utente normale – utile per controllare cosa ogni utente può fare.  
- Sanitizzazione input / prevenzione attacchi comuni (es. SQL injection, XSS) da implementare / verificare.

---

## ℹ Dettagli Extra

- Licenza: da definire.  
- Linguaggi prevalenti: HTML, C#, CSS, JS. ([github.com](https://github.com/Digizzz/PlayVault))
