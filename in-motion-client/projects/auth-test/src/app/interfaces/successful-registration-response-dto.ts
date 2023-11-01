export interface SuccessfulRegistrationResponseDto {
    status: number;
    serverResponseTime: Date;
    serverRequestTime: Date;
    data: { email: string };
}
