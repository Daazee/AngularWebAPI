import {AuthConfig} from 'angular-oauth2-oidc';
export const authConfig:AuthConfig={
    clientId:'self',
    oidc:false,
    tokenEndpoint:'http://localhost:18558/Token',
    userinfoEndpoint:"http://localhost:18558/api/accounts/profile/"
}