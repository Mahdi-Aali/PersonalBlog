import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";

@Injectable({providedIn: "root"})
export class ServiceBase
{

  public _httpClient: HttpClient | undefined = undefined;

  constructor(private httpClient: HttpClient) {
    this._httpClient = httpClient;
  }

}
