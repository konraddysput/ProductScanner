package com.productScanner;

import com.productScanner.model.ImageClasificationEventResultEntry;
import com.productScanner.model.ImagePreprocessingEvent;
import com.productScanner.semanticModel.Product;
import com.productScanner.services.WebSemanticService;
import org.apache.jena.rdf.model.*;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.TimeoutException;

public class Main {

    public static void main(String[] args) throws IOException, TimeoutException, IllegalAccessException {
//        EventBusService eventBusService = new EventBusService();
//        eventBusService.listen();

        WebSemanticService service=  new WebSemanticService();
        ImagePreprocessingEvent event= new ImagePreprocessingEvent();
        event.Id = 1;

        List<ImageClasificationEventResultEntry> data = new ArrayList<>();
        ImageClasificationEventResultEntry pepsi = new ImageClasificationEventResultEntry();
        pepsi.Category = "papsi";
        pepsi.Id = 1;
        pepsi.Position= new double[] {0.12,0.124124, 0.62464, 0.7573234};
        pepsi.Score =1;

        ImageClasificationEventResultEntry cola = new ImageClasificationEventResultEntry();
        pepsi.Category = "cola";
        pepsi.Id = 2;
        pepsi.Position= new double[] {0.12,0.124124, 0.62464, 0.7573234};
        pepsi.Score =0.95;

        data.add(pepsi);
        data.add(cola);
        event.Entries =data.toArray(new ImageClasificationEventResultEntry[data.size()]);

        service.findInvalidPositions(event);

    }
}
