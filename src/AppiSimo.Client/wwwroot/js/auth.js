(function(self) {

    self.authentication = {};

    const _manager = (() => {
        const config = {
            authority: 'https://cognito-idp.eu-central-1.amazonaws.com/eu-central-1_jUNe13QJ4',
            client_id: 'ld3qolihulq7pg0meehtfv20e',

            automaticSilentRenew: true,
            redirect_uri: 'https://localhost:5003/signed-in',
            post_logout_redirect_uri: '/',
            response_type: 'token',
            scope: 'openid',
            filterProtocolClaims: true,
            loadUserInfo: true,
            prompt: 'login',
            userStore: new Oidc.WebStorageStateStore({ store: localStorage})
        };

        return new Oidc.UserManager(config);
    })();

    const buildResponse = (user, error) => user
        ? {
            value: user
        }
        : {
            error: error
        };
    
    self.authentication.tryLoadUser = async () => buildResponse(await _manager.getUser());
    
    self.authentication.signIn = async () => {

        await _manager.clearStaleState();

        // HACK: fixes aws non standard response_type
        const r = await _manager.createSigninRequest();
        (Reflect.getPrototypeOf(r).constructor).isOidc = () => true;

        await _manager.signinRedirect({
            state: window.location.href,
        });
    };

    self.authentication.signedIn = async () => {
        const user = await _manager.signinRedirectCallback();

        return buildResponse(user, "An Error Occured")
    };
    
    self.authentication.signOut = () => _manager.signoutRedirect();

})(window.interop || (window.interop = {}));