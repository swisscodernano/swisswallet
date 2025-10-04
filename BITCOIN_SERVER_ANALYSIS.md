# 🔍 Bitcoin Core Server Analysis - 136.243.145.234

## 📋 **CHECKLIST ANALISI PRE-MODIFICA**

### **STEP 1: Verifiche di base**

```bash
# 1.1 Connessione e accesso
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "echo 'Connected successfully'"

# 1.2 Verifica Bitcoin Core attivo
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "ps aux | grep bitcoin"
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "systemctl status bitcoind"

# 1.3 Verifica porte in ascolto
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "netstat -tlnp | grep ':8332\|:48332'"
```

### **STEP 2: Analisi configurazione Bitcoin Core**

```bash
# 2.1 Localizza bitcoin.conf
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "find / -name 'bitcoin.conf' 2>/dev/null"

# 2.2 Esamina configurazione RPC esistente (SENZA modificare)
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "cat ~/.bitcoin/bitcoin.conf | grep -E 'rpc|server|bind'"

# 2.3 Verifica utenti RPC esistenti
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "cat ~/.bitcoin/bitcoin.conf | grep -E 'rpcuser|rpcauth'"
```

### **STEP 3: Analisi Tor configuration**

```bash
# 3.1 Verifica Tor installato e attivo
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "systemctl status tor"

# 3.2 Esamina configurazione Tor esistente
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "cat /etc/tor/torrc | grep -A 10 -B 2 HiddenService"

# 3.3 Lista servizi onion esistenti
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "ls -la /var/lib/tor/ | grep Hidden"

# 3.4 Verifica onion addresses esistenti
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "find /var/lib/tor -name 'hostname' -exec echo '{}:' \; -exec cat {} \;"
```

### **STEP 4: Analisi sicurezza e firewall**

```bash
# 4.1 Verifica firewall rules
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "ufw status verbose"

# 4.2 Verifica iptables rules
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "iptables -L -n | grep '8332\|48332'"

# 4.3 Verifica connessioni attive RPC
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "netstat -an | grep ':8332\|:48332'"
```

### **STEP 5: Test connettività esistente**

```bash
# 5.1 Test RPC locale (se credenziali note)
ssh -i /home/gemini-admin/.ssh/ser root@136.243.145.234 "bitcoin-cli getblockchaininfo"

# 5.2 Test onion service coordinator (conferma funzionamento)
curl --socks5 127.0.0.1:9050 -m 10 "http://rhuvjl2kosdi3xgnmkr4bwnvpmlsvupajkubuazxendgtorvi2q4nhyd.onion"
```

## 📊 **RISULTATI ATTESI**

### ✅ **Cosa DOVREMMO trovare:**
- Bitcoin Core attivo e funzionante
- RPC configurato per Wasabi
- Tor con onion service per coordinator
- Firewall configurato correttamente

### ⚠️ **Cosa POTREMMO trovare:**
- Bitcoin Core già con onion service per RPC
- Multiple configurazioni RPC
- Tor proxy per Bitcoin Core
- Setup custom non standard

### 🚨 **Cosa NON dovremmo toccare:**
- Configurazione RPC esistente per Wasabi
- Onion service del coordinator
- Firewall rules esistenti
- Bitcoin Core systemd service

## 🎯 **PROSSIMI PASSI POST-ANALISI**

1. **Se Bitcoin Core ha già onion RPC**: Useremo quello esistente
2. **Se non ha onion RPC**: Creeremo nuovo servizio dedicato
3. **Se setup custom**: Adatteremo la configurazione

**STATUS**: 🔍 Pronto per analisi dettagliata
**RISCHIO**: 🟢 Zero (solo lettura)
**BACKUP**: Non necessario (nessuna modifica)