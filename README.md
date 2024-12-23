EmbyClearLogos
An application to remove Live TV Guide Logos (Icons) from your Emby server.

Usage:
    EmbyClearLogos API_KEY {server} {port}

Parameters:
    API_KEY  - (Mandatory) Emby server API key. To generate an API key, go to your Emby dashboard > advanced > security and generate one.
    server   - (Optional) Emby server IP address. Default is `localhost` if not specified.
    port     - (Optional) Emby server port. Default is `8096` if not specified.

Examples:

Running on the Emby server with the default port:
    EmbyClearLogos api-key

Running from a remote machine:
    EmbyClearLogos api-key 192.168.50.50 8000

Description:
    EmbyClearLogos connects to your Emby server and removes logos (icons) for all channels in the Live TV Guide.
    The application uses the Emby API and requires a valid API key for authentication.
