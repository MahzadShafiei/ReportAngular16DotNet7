export interface filterParameter {    
    hallType: HallType | undefined;
    hallCode: string | undefined;
    startDate: string | null;
    endDate: string | null;
    meter : Meter | undefined;  
    period:  string| undefined; 
}

export enum HallType {
    Paint = 1, 
    Body = 2,
}

export enum Meter
    {
        Water = 1,
        Gas = 2,
        CompresAir = 4,
        Electricity = 5,
    }