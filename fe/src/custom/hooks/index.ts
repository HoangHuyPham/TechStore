import { useEffect, useState } from "react"

const useDebounce = (nextData: any, timeout=1000)=>{
    const [data, setData] = useState(nextData)
    useEffect(()=>{
        const timeoutHandle = setTimeout(() => {
            setData(nextData)
        }, timeout);

        return ()=> clearTimeout(timeoutHandle)
    }, [nextData, timeout])

    return data;
}

export {
    useDebounce
}