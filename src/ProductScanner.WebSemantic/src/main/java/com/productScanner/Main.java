package com.productScanner;

import com.productScanner.semanticModel.Product;
import com.productScanner.services.WebSemanticService;
import org.apache.jena.rdf.model.*;

import java.io.IOException;
import java.util.concurrent.TimeoutException;

public class Main {

    public static void main(String[] args) throws IOException, TimeoutException, IllegalAccessException {
//        EventBusService eventBusService = new EventBusService();
//        eventBusService.listen();

        WebSemanticService service=  new WebSemanticService();
        service.printModel();
        service.find();


    }
}
