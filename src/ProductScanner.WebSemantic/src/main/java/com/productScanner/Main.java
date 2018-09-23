package com.productScanner;

import com.productScanner.model.ImageClasificationEventResultEntry;
import com.productScanner.model.ImagePreprocessingEvent;
import com.productScanner.services.WebSemanticService;

import java.io.IOException;
import java.util.ArrayList;
import java.util.concurrent.TimeoutException;

public class Main {

    public static void main(String[] args) throws IOException, TimeoutException, IllegalAccessException {
//        EventBusService eventBusService = new EventBusService();
//        eventBusService.listen();

        WebSemanticService service=  new WebSemanticService();
        ImagePreprocessingEvent event= new ImagePreprocessingEvent();
        event.Id = 12412;

        ArrayList<ImageClasificationEventResultEntry> data = new ArrayList<>();
        ImageClasificationEventResultEntry pepsi = new ImageClasificationEventResultEntry();
        pepsi.Category = "pepsi";
        pepsi.Id = 123;
        pepsi.Position= new double[] {0.12,0.124124, 0.62464, 0.7573234};
        pepsi.Score =1;

        ImageClasificationEventResultEntry cola = new ImageClasificationEventResultEntry();
        cola.Category = "cola";
        cola.Id = 312;
        cola.Position= new double[] {0.12,0.124124, 0.62464, 0.7573234};
        cola.Score =0.95;

        data.add(pepsi);
        data.add(cola);
        event.Entries =data.toArray(new ImageClasificationEventResultEntry[data.size()]);

        service.addToOntology(data, event.Id);
        service.startResoing();
        System.out.println(service.getResult(data));
    }
}
