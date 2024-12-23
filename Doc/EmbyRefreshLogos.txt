EmbyRefreshLogos
An application to remove Live TV Guide Logos (Icons) from your Emby server.

Usage:
    EmbyRefreshLogos {API_KEY} {SCHEDULED_TASK_ID} {server} {port}

Parameters:
    API_KEY           - (Mandatory) Emby server API key. To generate an API key, go to your Emby dashboard > advanced > security and generate one.
    SCHEDULED_TASK_ID - (Mandatory) The task ID for the Guide Refresh operation. Typically available from your Emby API or documentation.
    server            - (Optional) Emby server IP address. Default is `localhost` if not specified.
    port              - (Optional) Emby server port. Default is `8096` if not specified.

Examples:
Running on the Emby server with the default port:
    EmbyRefreshLogos {api-key} {scheduled-task-id}

Running from a remote machine:
    EmbyRefreshLogos {api-key} {scheduled-task-id} {IP ADDRESS} {PORT}

Description:
    EmbyRefreshLogos connects to your Emby server and removes logos (icons) for all channels in the Live TV Guide.

    After clearing the logos, the application automatically triggers the Guide Refresh operation to update the Live TV Guide.

    The application uses the Emby API and requires a valid API key for authentication.
