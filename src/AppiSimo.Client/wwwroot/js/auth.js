(function(self) {

    self.authentication = {};
    
    self.authentication.signIn = (username, password, config) => {

        const authenticationDetails = new AmazonCognitoIdentity.AuthenticationDetails({
            Username: username,
            Password: password
        });

        const cognitoUser = getCognitoUser(username, config.userPoolId, config.clientId);
        
        return new Promise((resolve, reject) => {
            cognitoUser.authenticateUser(authenticationDetails, {
                onSuccess: function (result) {
                    const token = result.getAccessToken().getJwtToken();
                    resolve({
                        value:{
                           value: token                            
                        }
                    });
                },
                onFailure: function(error) {
                    resolve({
                        error: {
                            type: error.code === 'NotAuthorizedException' ? 2 : 0,
                            message: error.message
                        }
                    });
                },
                newPasswordRequired: function (userAttributes, requiredAttributes) {
                    resolve({
                        error: {
                            type: 1,
                            message: "New password required."
                        }
                    });                    
                }
            });
        });
    };
    
    self.authentication.signOut = (username, config) => {
        const cognitoUser = getCognitoUser(username, config.userPoolId, config.clientId);
        cognitoUser.signOut();
    };

    self.authentication.completeNewPasswordChallenge = (username, oldPassword, newPassword, config) => {

        const cognitoUser = getCognitoUser(username, config.userPoolId, config.clientId);        
        
        return new Promise((resolve, reject) => {
            function completeNewPasswordChallenge(){
                return cognitoUser.completeNewPasswordChallenge(newPassword, {"name": name}, {
                    onSuccess: result => {
                        resolve();
                    },
                    onFailure: err => {
                        reject(err);
                    }
                });
            }

            const authenticationDetails = new AmazonCognitoIdentity.AuthenticationDetails({
                Username: username,
                Password: oldPassword
            });
            
            cognitoUser.authenticateUser(authenticationDetails, {
                onSuccess: function (result) {
                    completeNewPasswordChallenge();
                },
                newPasswordRequired: function (userAttributes, requiredAttributes) {
                    completeNewPasswordChallenge();
                },
                onFailure: function(error) {
                    reject(error);
                }
            });
            
            
        });
    };
    
    const getCognitoUser = (username, userPoolId, clientId) => new AmazonCognitoIdentity.CognitoUser({
        Username: username,
        Pool: new AmazonCognitoIdentity.CognitoUserPool({
            UserPoolId: userPoolId,
            ClientId: clientId
        })
    });

})(window.interop || (window.interop = {}));