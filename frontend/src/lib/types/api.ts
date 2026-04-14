export interface ServiceResponse<T> {
  data: T | null;
  message: string;
  success: boolean;
  errors: string[] | null;
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  username: string;
  email: string;
  role: UserRole;
}

export enum UserRole {
  Admin = 1,
  Staff = 2,
  Customer = 3
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
}
 export interface booking{
  id:number;
  roomId: number;
  guestName:string;
  guestEmail:string;
  checkIn:string; 
  checkOut:string;
  totalPrice:number;
  discountAmount:number;
  finalprice:number;
  status:string;
  numberOfPeople:number;
  createdat:number;
}

export interface BookingCreateRequest{
  id:number;
  roomId: number;
  guestName:string;
  guestEmail:string;
  checkIn:string;
  checkOut:string;
  totalPrice:number;
  discountAmount:number;
  finalprice:number;
  status:string;
  numberOfPeople:number;
  createdat:number;
}
  
 export interface BookingpdateRequest extends BookingCreateRequest {
  id: number;
}


export interface Room {
  id: number;
  name: string;
  location: string;
  description: string;
  pricePerNight: number;
  capacity: number;
  isAvailable: boolean;
  viewType: string;
  amenities: string[];
  imageUrls: string[];
  bedType: string;
  sizeSqFt: number;
}

export interface RoomCreateRequest {
  name: string;
  location: string;
  description: string;
  pricePerNight: number;
  capacity: number;
  isAvailable: boolean;
  viewType: string;
  amenities: string[];
  imageUrls: string[];
  bedType: string;
  sizeSqFt: number;
}

export interface RoomUpdateRequest extends RoomCreateRequest {
  id: number;
}

export interface Offer {
  id: number;
  title: string;
  description: string;
  discountType: string;
  discountValue: number;
  startDate: string;
  endDate: string;
  isActive: boolean;
  appliesToAllRooms: boolean;
  roomIds: number[];
  imageUrl: string;
}

export interface OfferRequest {
  title: string;
  description: string;
  discountType: string;
  discountValue: number;
  startDate: string;
  endDate: string;
  isActive: boolean;
  appliesToAllRooms: boolean;
  roomIds: number[];
  imageUrl: string;
}

export interface GalleryItem {
  id: number;
  imageUrl: string;
  altText?: string;
  category: string;
  displayOrder: number;
  createdAt: string;
}

export interface GalleryItemRequest {
  imageUrl: string;
  altText?: string;
  category: string;
  displayOrder: number;
}

