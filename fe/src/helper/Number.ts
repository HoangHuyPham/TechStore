class Number {
    static formatter = new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND',
        minimumFractionDigits: 0,
    })
    static formatPrice = (val:number):string=>{
       return Number.formatter.format(val)
    }
}


export default Number