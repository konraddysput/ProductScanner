package com.productScanner.services;

import com.productScanner.model.ImagePreprocessingEvent;
import com.productScanner.semanticModel.Product;
import com.productScanner.semanticModel.ProductRule;
import com.productScanner.semanticModel.RuleType;
import com.sun.org.apache.xpath.internal.operations.Bool;
import org.apache.jena.rdf.model.*;
import org.apache.jena.reasoner.rulesys.Rule;
import org.apache.jena.util.FileManager;

import java.io.FileNotFoundException;
import java.io.InputStream;
import java.io.PrintWriter;
import java.util.ArrayList;
import java.util.List;

public class WebSemanticService {
    private Model _model = ModelFactory.createDefaultModel();
    private final String uri = "http://productScanner/";

    public  WebSemanticService() throws IllegalAccessException {
        loadData();
    }

    public void saveModel(){
        try (PrintWriter out = new PrintWriter("knowledge-base.xml")) {
            _model.write(out, "RDF/XML-ABBREV");
        } catch (FileNotFoundException e) {
            e.printStackTrace();
        }
    }
    public void printModel(){
        StmtIterator iter = _model.listStatements();
        // print out the predicate, subject and object of each statement
        while (iter.hasNext()) {
            Statement stmt      = iter.nextStatement();  // get next statement
            Resource subject   = stmt.getSubject();     // get the subject
            Property predicate = stmt.getPredicate();   // get the predicate
            RDFNode object    = stmt.getObject();      // get the object

            System.out.print(subject.toString());
            System.out.print(" " + predicate.toString() + " ");
            if (object instanceof Resource) {
                System.out.print(object.toString());
            } else {
                // object is a literal
                System.out.print(" \"" + object.toString() + "\"");
            }

            System.out.println(" .");
        }

        System.out.println(_model.toString());
    }

    private void loadData() throws IllegalAccessException {
        // use the FileManager to find the input file
        InputStream in = FileManager.get().open( "knowledge-base.xml" );
        if (in == null) {
            System.out.println("Data not exists. Creating new RDF knowledge base");
            Resource resource = _model.createResource("http://productScanner/");

            for(Product product: generateDefaultProducts()){
                Property property = _model.createProperty(uri, "contain");
                resource.addProperty(property, product.loadResources(_model.createResource(uri + product.classifier), _model));
            }
            saveModel();
            return;
        }
        System.out.println("Loading knowledge base from RDF file");
        _model.read(in, "RDF/XML-ABBREV");
    }

    private List<Product> generateDefaultProducts(){
        List<ProductRule> colaRules = new ArrayList<ProductRule>();
        colaRules.add(new ProductRule("pepsiCloseToCola", "pepsi", RuleType.Close, "cola", 50));
        colaRules.add(new ProductRule("colaOnBottom", "cola", RuleType.OnBottom));

        Product cola = new Product(1, "cola", 51,141, colaRules);

        List<ProductRule> pepsiRules = new ArrayList<ProductRule>();
        pepsiRules.add(new ProductRule("ColaZeroOnPicture", "pepsi", RuleType.InTheSamePicture, "colazero", 0));
        Product pepsi = new Product(2,"pepsi", 51,140, pepsiRules);

        List<Product> result = new ArrayList<>();
        result.add(pepsi);
        result.add(cola);

        return result;

    }
    public boolean find(){
        Selector selector = new SimpleSelector(null, null, "cola");
        StmtIterator iter = _model.listStatements(selector);
        if (iter.hasNext()) {
            System.out.println("The database contains colas:");
            while (iter.hasNext()) {
                System.out.println("  " + iter.nextStatement());
            }
        } else {
            System.out.println("No vcards were found in the database");
        }
        return iter.hasNext();
    }


    public void findInvalidPositions(ImagePreprocessingEvent data){
        return;
    }
}
