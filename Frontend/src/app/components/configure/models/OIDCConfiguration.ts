export interface OIDCConfiguration {
    id: string | null,
    authority: string | null,
    audience: string | null,
    callback_uri: string | null,
    certificatePassword: string | null,
    certificateSerial: string | null,
    client_id: string | null,
    redirect_uri: string | null,
    response_type: string | null,
    scope: string | null
}