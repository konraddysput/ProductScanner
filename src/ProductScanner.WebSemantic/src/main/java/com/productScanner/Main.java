package com.productScanner;

import com.productScanner.services.EventBusService;

import java.io.IOException;
import java.util.concurrent.TimeoutException;

public class Main {

    public static void main(String[] args) throws IOException, TimeoutException {
        EventBusService eventBusService = new EventBusService();
        eventBusService.listen();
    }
}
