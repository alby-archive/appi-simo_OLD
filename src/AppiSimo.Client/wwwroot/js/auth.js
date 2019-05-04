(function(self) {

    const buildManager = config => {

        config = JSON.parse(config);
        config.userStore = new Oidc.WebStorageStateStore({store: localStorage});
        
        return new Oidc.UserManager(config);
    };
    
    const invokeAsync = async (config, func) => {
        
        const manager = buildManager(config);

        const user = await func(manager);
        
        return JSON.stringify(user);
    };
    
    self.authentication = {};
    
    self.authentication.tryLoadUser = async config => invokeAsync(config, async manager => await manager.getUser());
    
    self.authentication.signIn = async config => {
        
        const manager = buildManager(config);
        
        await manager.clearStaleState();

        // HACK: fixes aws non standard response_type
        const r = await manager.createSigninRequest();
        (Reflect.getPrototypeOf(r).constructor).isOidc = () => true;

        manager.signinRedirect({
            state: window.location.href,
        });
    };

    self.authentication.signedIn = async config => invokeAsync(config, async manager => await manager.signinRedirectCallback());
    
    self.authentication.signOut = config => buildManager(config).signoutRedirect();

})(window.interop || (window.interop = {}));
    