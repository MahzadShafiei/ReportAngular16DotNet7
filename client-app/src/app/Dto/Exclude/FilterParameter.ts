export interface filterParameter {    
    hallType: HallType | undefined;
    hallCode: string | undefined;
    startDate: string | null;
    endDate: string | null;
    meter : Meter | undefined;  
    period:  Period| undefined; 
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

export enum Period
    {
        Minute = 0,
        Hour = 1,
        Day = 2,
        Month = 3,
    }