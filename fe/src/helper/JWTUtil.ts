import * as jose from 'jose'
import moment from 'moment'

class JWTUtil {
    static isValidJWT = ():boolean => {
        const jwtToken = localStorage.getItem("jwt");
        if (!jwtToken) return false
        const jwtDecoded = jose.decodeJwt(jwtToken)

        if (jwtDecoded.exp && jwtDecoded?.exp * 1000 > moment.now()) {
            return true
        }
        return false
    }
}


export default JWTUtil