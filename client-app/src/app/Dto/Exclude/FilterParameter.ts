import { unitModel } from "src/app/models/unitModel";

export interface filterParameter {
    assisttanceType: number | undefined;
    managementType: number | undefined;
    hallType: number | undefined;
    hallCode: string | undefined;
    startDate: string | null;
    endDate: string | null;
    meter: Meter | undefined;
    period: Period | undefined;
}

export enum Meter {
    Water = 1,
    Gas = 2,
    CompresAir = 4,
    Electricity = 5,
}

export enum Period {
    Minute = 0,
    Hour = 1,
    Day = 2,
    Month = 3,
}