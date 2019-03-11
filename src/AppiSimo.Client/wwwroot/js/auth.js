(function(self) {

    self.authentication = {};
    
    self.authentication.signIn = (username, password, config) => {

        const cognitoUser = getCognitoUser(username, config.userPoolId, config.clientId);
        
        return new Promise((resolve, _) => {
            cognitoUser.authenticateUser(getAuthenticationDetails(username, password), {
                onSuccess: (result) => {
                    const token = result.getAccessToken().getJwtToken();
                    resolve({
                        value:{
                           value: token                            
                        }
                    });
                },
                onFailure: (error) => {
                    resolve({
                        error: {
                            type: error.code === 'NotAuthorizedException' ? 2 : 0,
                            message: error.message
                        }
                    });
                },
                newPasswordRequired: () => {
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
            
            const completeNewPasswordChallenge = () => {
                return cognitoUser.completeNewPasswordChallenge(newPassword, {"name": name}, {
                    onSuccess: () => {
                        resolve();
                    },
                    onFailure: err => {
                        reject(err);
                    }
                });
            };
            
            cognitoUser.authenticateUser(getAuthenticationDetails(username, oldPassword), {
                onSuccess: () => {
                    completeNewPasswordChallenge();
                },
                newPasswordRequired: () => {
                    completeNewPasswordChallenge();
                },
                onFailure: (error) => {
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

    const getAuthenticationDetails = (username, password) => new AmazonCognitoIdentity.AuthenticationDetails({
        Username: username,
        Password: password
    });
    
})(window.interop || (window.interop = {}));