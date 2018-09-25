import { PhotoObjectViewModel } from "./photoObjectViewModel";

export class PhotoDetailViewModel {
    public id: number;
    public uploadDate: Date;
    public photoObjects: PhotoObjectViewModel[];  
    public hourAgo: string;
}