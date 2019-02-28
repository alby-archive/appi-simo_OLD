(function(self) {

    self.authentication = {};
    
    self.authentication.signIn = (username, password, config) => {

        const authenticationData = {
            Username: username,
            Password: password
        };
        
        const poolData = {
            UserPoolId: config.userPoolId,
            ClientId: config.clientId
        };
        
        const userData = {
            Username: username,
            Pool: new AmazonCognitoIdentity.CognitoUserPool(poolData)
        };

        const authenticationDetails = new AmazonCognitoIdentity.AuthenticationDetails(authenticationData);

        const cognitoUser = new AmazonCognitoIdentity.CognitoUser(userData);
        
        let accessToken;
        
        return new Promise((resolve, reject) => {
            cognitoUser.authenticateUser(authenticationDetails, {
                onSuccess: function (result) {
                    accessToken = result.getAccessToken().getJwtToken();
                    resolve(accessToken);
                },

                onFailure: function(err) {
                    reject(err);
                },
                mfaRequired: function(codeDeliveryDetails) {
                    var verificationCode = prompt('Please input verification code' ,'');
                    cognitoUser.sendMFACode(verificationCode, this);
                },
                newPasswordRequired: function (err) {
                    resolve("newPasswordRequired");
                }
            });
        });
    };

})(window.interop || (window.interop = {}));