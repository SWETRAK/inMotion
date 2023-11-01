export interface ImsHttpMessage<T> {
    status: number;
    serverResponseTime: Date;
    serverRequestTime: Date;
    data: T;
}
