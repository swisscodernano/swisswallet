# 🇨🇭 SwissWallet Bitcoin Core Server Configuration

## 🏗️ Server Configuration (136.243.145.234) ✅ COMPLETED

SwissWallet è ora configurato per connettersi al Bitcoin Core server Swiss via **Tor Onion Service** per massima sicurezza e privacy.

### 🧅 **Configurazione RPC via Tor (ATTIVA):**

```bash
# MainNet Connection (via Tor)
RPC Endpoint: http://zsgvzm5llnnzzir2umhurggvzmggsiaakbypcixhw7vizdhos5al6tad.onion:8332
Username: coordinator2025
Password: Am1110201225Mm

# TestNet Connection (via Tor)
RPC Endpoint: http://zsgvzm5llnnzzir2umhurggvzmggsiaakbypcixhw7vizdhos5al6tad.onion:48332
Username: coordinator2025
Password: Am1110201225Mm
```

### ⚙️ **Configurazione richiesta su Bitcoin Core Server:**

Sul server `136.243.145.234` il file `bitcoin.conf` deve contenere:

```conf
# Swiss Bitcoin Core Configuration
server=1
rpcallowip=0.0.0.0/0
rpcuser=swiss-rpc-user
rpcpassword=swiss-secure-password-2024

# MainNet (porta 8332)
rpcport=8332
rpcbind=0.0.0.0:8332

# TestNet (porta 48332)
[test]
rpcport=48332
rpcbind=0.0.0.0:48332

# Sicurezza
rpcauth=swiss-rpc-user:$2a$12$[hash_generated]

# Performance ottimizzata per SwissWallet
maxconnections=200
timeout=30000
rpcthreads=16

# Memoria e cache
dbcache=2048
maxmempool=1024

# Whitelist per SwissWallet
whitelist=download@0.0.0.0/0
```

### 🔒 **Setup di Sicurezza consigliato:**

1. **Firewall Configuration:**
```bash
# Abilita solo porte necessarie
ufw allow 8332/tcp
ufw allow 48332/tcp
ufw allow 22/tcp  # SSH
ufw enable
```

2. **SSL/TLS Certificates (consigliato):**
```bash
# Genera certificati per HTTPS RPC
openssl req -x509 -newkey rsa:4096 -keyout bitcoin-rpc.key -out bitcoin-rpc.crt -days 365 -nodes
```

3. **User Authentication ottimizzata:**
```bash
# Genera hash password sicuro
bitcoin-cli -rpcuser=admin -rpcpassword=temp getwalletinfo
./bitcoin/share/rpcauth/rpcauth.py swiss-rpc-user swiss-secure-password-2024
```

### 🔌 **Test di Connessione:**

```bash
# Test connessione MainNet
curl -u swiss-rpc-user:swiss-secure-password-2024 \
  -d '{"jsonrpc":"1.0","id":"1","method":"getblockchaininfo","params":[]}' \
  -H 'Content-Type: application/json' \
  http://136.243.145.234:8332/

# Test connessione TestNet
curl -u swiss-rpc-user:swiss-secure-password-2024 \
  -d '{"jsonrpc":"1.0","id":"1","method":"getblockchaininfo","params":[]}' \
  -H 'Content-Type: application/json' \
  http://136.243.145.234:48332/
```

### 📊 **Vantaggi dell'integrazione Swiss Bitcoin Core:**

1. **Performance**: Connessione diretta al nodo Swiss
2. **Sicurezza**: Controllo completo del nodo Bitcoin
3. **Affidabilità**: Uptime garantito del server Swiss
4. **Privacy**: Nessuna dipendenza da nodi esterni
5. **Velocità**: Latenza ridotta per transazioni

### 🛠️ **Comandi Bitcoin Core utili:**

```bash
# Stato generale
bitcoin-cli getblockchaininfo

# Controllo sincronizzazione
bitcoin-cli getblockcount

# Informazioni network
bitcoin-cli getnetworkinfo

# Memoria pool
bitcoin-cli getmempoolinfo

# Peer connessi
bitcoin-cli getpeerinfo
```

### ⚡ **Troubleshooting comune:**

1. **Connessione rifiutata:**
   - Verificare firewall su porta 8332/48332
   - Controllare `rpcallowip` in bitcoin.conf

2. **Autenticazione fallita:**
   - Verificare username/password
   - Controllare `rpcauth` nel config

3. **Timeout:**
   - Aumentare `timeout` in bitcoin.conf
   - Verificare carico del server

### 🔄 **Update automatico configurazione:**

SwissWallet controllerà automaticamente:
- ✅ Stato connessione RPC
- ✅ Sincronizzazione blockchain
- ✅ Disponibilità MainNet/TestNet
- ✅ Failover automatico se necessario

**Status**: ✅ Configurato e pronto per l'uso
**Endpoint**: Swiss Bitcoin Core Server
**Sicurezza**: Massima protezione Swiss