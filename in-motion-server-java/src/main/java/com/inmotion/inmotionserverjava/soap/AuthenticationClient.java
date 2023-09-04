package com.inmotion.inmotionserverjava.soap;

import com.inmotion.soap.wsdl.ValidateJwtToken;
import com.inmotion.soap.wsdl.ValidateJwtTokenResponse;
import org.springframework.ws.client.core.support.WebServiceGatewaySupport;
import org.springframework.ws.soap.client.core.SoapActionCallback;

public class AuthenticationClient extends WebServiceGatewaySupport {

    public ValidateJwtTokenResponse validateJwtToken(String jwtToken) {
        ValidateJwtToken request = new ValidateJwtToken();
        request.setJwtToken(jwtToken);

        return (ValidateJwtTokenResponse) getWebServiceTemplate().marshalSendAndReceive(
                "http://localhost:8001/soap/jwtService.asmx", request,
                new SoapActionCallback("http://tempuri.org/IValidateJwtTokenSoap/ValidateJwtTokenResponse")
        );
    }
}
