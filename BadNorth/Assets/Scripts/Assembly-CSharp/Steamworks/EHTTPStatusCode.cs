using System;

namespace Steamworks
{
	// Token: 0x02000317 RID: 791
	public enum EHTTPStatusCode
	{
		// Token: 0x04000BAC RID: 2988
		k_EHTTPStatusCodeInvalid,
		// Token: 0x04000BAD RID: 2989
		k_EHTTPStatusCode100Continue = 100,
		// Token: 0x04000BAE RID: 2990
		k_EHTTPStatusCode101SwitchingProtocols,
		// Token: 0x04000BAF RID: 2991
		k_EHTTPStatusCode200OK = 200,
		// Token: 0x04000BB0 RID: 2992
		k_EHTTPStatusCode201Created,
		// Token: 0x04000BB1 RID: 2993
		k_EHTTPStatusCode202Accepted,
		// Token: 0x04000BB2 RID: 2994
		k_EHTTPStatusCode203NonAuthoritative,
		// Token: 0x04000BB3 RID: 2995
		k_EHTTPStatusCode204NoContent,
		// Token: 0x04000BB4 RID: 2996
		k_EHTTPStatusCode205ResetContent,
		// Token: 0x04000BB5 RID: 2997
		k_EHTTPStatusCode206PartialContent,
		// Token: 0x04000BB6 RID: 2998
		k_EHTTPStatusCode300MultipleChoices = 300,
		// Token: 0x04000BB7 RID: 2999
		k_EHTTPStatusCode301MovedPermanently,
		// Token: 0x04000BB8 RID: 3000
		k_EHTTPStatusCode302Found,
		// Token: 0x04000BB9 RID: 3001
		k_EHTTPStatusCode303SeeOther,
		// Token: 0x04000BBA RID: 3002
		k_EHTTPStatusCode304NotModified,
		// Token: 0x04000BBB RID: 3003
		k_EHTTPStatusCode305UseProxy,
		// Token: 0x04000BBC RID: 3004
		k_EHTTPStatusCode307TemporaryRedirect = 307,
		// Token: 0x04000BBD RID: 3005
		k_EHTTPStatusCode400BadRequest = 400,
		// Token: 0x04000BBE RID: 3006
		k_EHTTPStatusCode401Unauthorized,
		// Token: 0x04000BBF RID: 3007
		k_EHTTPStatusCode402PaymentRequired,
		// Token: 0x04000BC0 RID: 3008
		k_EHTTPStatusCode403Forbidden,
		// Token: 0x04000BC1 RID: 3009
		k_EHTTPStatusCode404NotFound,
		// Token: 0x04000BC2 RID: 3010
		k_EHTTPStatusCode405MethodNotAllowed,
		// Token: 0x04000BC3 RID: 3011
		k_EHTTPStatusCode406NotAcceptable,
		// Token: 0x04000BC4 RID: 3012
		k_EHTTPStatusCode407ProxyAuthRequired,
		// Token: 0x04000BC5 RID: 3013
		k_EHTTPStatusCode408RequestTimeout,
		// Token: 0x04000BC6 RID: 3014
		k_EHTTPStatusCode409Conflict,
		// Token: 0x04000BC7 RID: 3015
		k_EHTTPStatusCode410Gone,
		// Token: 0x04000BC8 RID: 3016
		k_EHTTPStatusCode411LengthRequired,
		// Token: 0x04000BC9 RID: 3017
		k_EHTTPStatusCode412PreconditionFailed,
		// Token: 0x04000BCA RID: 3018
		k_EHTTPStatusCode413RequestEntityTooLarge,
		// Token: 0x04000BCB RID: 3019
		k_EHTTPStatusCode414RequestURITooLong,
		// Token: 0x04000BCC RID: 3020
		k_EHTTPStatusCode415UnsupportedMediaType,
		// Token: 0x04000BCD RID: 3021
		k_EHTTPStatusCode416RequestedRangeNotSatisfiable,
		// Token: 0x04000BCE RID: 3022
		k_EHTTPStatusCode417ExpectationFailed,
		// Token: 0x04000BCF RID: 3023
		k_EHTTPStatusCode4xxUnknown,
		// Token: 0x04000BD0 RID: 3024
		k_EHTTPStatusCode429TooManyRequests = 429,
		// Token: 0x04000BD1 RID: 3025
		k_EHTTPStatusCode500InternalServerError = 500,
		// Token: 0x04000BD2 RID: 3026
		k_EHTTPStatusCode501NotImplemented,
		// Token: 0x04000BD3 RID: 3027
		k_EHTTPStatusCode502BadGateway,
		// Token: 0x04000BD4 RID: 3028
		k_EHTTPStatusCode503ServiceUnavailable,
		// Token: 0x04000BD5 RID: 3029
		k_EHTTPStatusCode504GatewayTimeout,
		// Token: 0x04000BD6 RID: 3030
		k_EHTTPStatusCode505HTTPVersionNotSupported,
		// Token: 0x04000BD7 RID: 3031
		k_EHTTPStatusCode5xxUnknown = 599
	}
}
