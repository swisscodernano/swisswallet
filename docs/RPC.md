# SwissWallet RPC Server

SwissWallet Daemon provides a JSON-RPC interface for programmatic interaction with your wallet.

## Enabling RPC Server

Add to your `Config.json`:

```json
{
  "JsonRpcServerEnabled": true,
  "JsonRpcUser": "your_username",
  "JsonRpcPassword": "your_secure_password",
  "JsonRpcServerPrefixes": [
    "http://127.0.0.1:37128/",
    "http://localhost:37128/"
  ]
}
```

Or use environment variables:

```bash
export WASABI_JSONRPCSERVERENABLED=true
export WASABI_JSONRPCUSER=your_username
export WASABI_JSONRPCPASSWORD=your_secure_password
```

Or command line arguments:

```bash
./swisswalletd --jsonrpcserverenabled=true --jsonrpcuser=your_username --jsonrpcpassword=your_password
```

## Using the RPC Server

### With wcli Script

The easiest way to interact with the RPC server is using the `wcli` script:

```bash
cd Contrib/CLI
./wcli --help
```

See [CLI documentation](../../Contrib/CLI/README.md) for details.

### Direct HTTP Requests

Make HTTP POST requests to `http://localhost:37128/`:

```bash
curl -X POST http://localhost:37128/ \
  -u your_username:your_password \
  -H "Content-Type: application/json" \
  -d '{
    "jsonrpc": "2.0",
    "id": "1",
    "method": "listwallets",
    "params": []
  }'
```

## Available RPC Methods

Common methods:

- `listwallets` - List all loaded wallets
- `getstatus` - Get wallet synchronization status
- `getbalance` - Get wallet balance
- `send` - Send bitcoin transaction
- `listunspentcoins` - List UTXOs
- `gethistory` - Get transaction history
- `getnewaddress` - Generate new receiving address
- `createwallet` - Create new wallet
- `loadwallet` - Load existing wallet
- `stopwallet` - Stop a running wallet

## Security Notes

⚠️ **Important:**

1. **Local access only** - By default, RPC server only accepts connections from localhost (127.0.0.1)
2. **Use strong passwords** - RPC username/password protect full wallet access
3. **No external exposure** - Never expose RPC port to public internet
4. **Swiss infrastructure** - SwissWallet connects only to Swiss coordinators via Tor

## Swiss Privacy Features

When using RPC server:

- All CoinJoin coordination happens through Swiss Tor onion services
- No telemetry or tracking
- Full control over your privacy settings
- Swiss jurisdiction protection

## Example: Creating and Loading a Wallet

```bash
# Create new wallet
./wcli createwallet MyWallet "my secure passphrase"

# Load wallet
./wcli loadwallet MyWallet

# Get new address
./wcli getnewaddress MyWallet "Donation address"

# Check balance
./wcli getbalance MyWallet
```

## More Information

- **CLI Tool**: [Contrib/CLI/README.md](../../Contrib/CLI/README.md)
- **Daemon Configuration**: [WalletWasabi.Daemon/README.md](../../WalletWasabi.Daemon/README.md)
- **Build from Source**: [docs/build/BUILD_QUICK_START.md](build/BUILD_QUICK_START.md)

---

🇨🇭 **SwissWallet** — Bitcoin Privacy with Swiss Precision
