package com.productScanner.services;

import com.productScanner.model.ImageClasificationEventResultEntry;
import openllet.jena.PelletReasonerFactory;
import org.apache.jena.datatypes.xsd.XSDDatatype;
import org.apache.jena.ontology.Individual;
import org.apache.jena.ontology.OntClass;
import org.apache.jena.ontology.OntModel;
import org.apache.jena.rdf.model.*;

import java.util.*;

public class WebSemanticService {
    private OntModel _model = ModelFactory.createOntologyModel(PelletReasonerFactory.THE_SPEC);
    private final String iri = "http://www.product-scanner/ontology";

    public  WebSemanticService() {
        loadData();
    }

    private void loadData() {
        System.out.println("Loading knowledge base from RDF file");
        _model.read("knowledge-base.owl" , "OWL/XML-ABBREV");
    }

    public void addToOntology(List<ImageClasificationEventResultEntry> entryList, int id){
        for (ImageClasificationEventResultEntry entry :entryList) {
            addToOntology(entry, id);
        }
    }

    public void addToOntology(ImageClasificationEventResultEntry entry, int imageId){
        OntClass product = _model.getOntClass(iri + "#Product");
        Individual individual =_model.createIndividual(iri +"#" + entry.Id, product);

        //setup image id
        Property hasImageId = _model.createProperty(iri + "#imageId");
        individual.addProperty(hasImageId, String.valueOf(imageId), XSDDatatype.XSDint);

        //setup name
        Property hasName = _model.createProperty(iri + "#productName");
        individual.addProperty(hasName, entry.Category, XSDDatatype.XSDstring);

        //setup position
        Property hasPositionYMin = _model.createProperty(iri + "#positionYMin");
        individual.addProperty(hasPositionYMin, String.valueOf(entry.Position[0]), XSDDatatype.XSDdouble);

        Property hasPositionXMin = _model.createProperty(iri + "#positionXMin");
        individual.addProperty(hasPositionXMin, String.valueOf(entry.Position[1]), XSDDatatype.XSDdouble);

        Property hasPositionYMax = _model.createProperty(iri + "#positionYMax");
        individual.addProperty(hasPositionYMax, String.valueOf(entry.Position[2]), XSDDatatype.XSDdouble);

        Property hasPositionXmax = _model.createProperty(iri + "#positionXMax");
        individual.addProperty(hasPositionXmax, String.valueOf(entry.Position[3]), XSDDatatype.XSDdouble);
    }

    public void startResoing(){
        _model.prepare();
    }

    public Map<Integer, Map<String, Object>> getResult(List<ImageClasificationEventResultEntry> entryList){
        Map<Integer, Map<String,Object>> result = new HashMap<>();
        for (ImageClasificationEventResultEntry entry: entryList) {
            Resource reasonerResource = _model.getResource(iri + "#" +  entry.Id);
            StmtIterator iter = reasonerResource.listProperties();
            Map<String, Object> entryResult = new HashMap<>();

            LinkedList<String> entries = new LinkedList<String>();
            while (iter.hasNext()) {

                Statement stmt      = iter.nextStatement();  // get next statement
                Property predicate = stmt.getPredicate();   // get the predicate
                RDFNode object    = stmt.getObject();      // get the object
                String predicateString = EscapeReasonerResult(predicate.toString());

                switch (predicateString){
                    case "differentFrom":
                        continue;
                    case "type":
                        entries.add(EscapeReasonerResult(object.toString()));
                        break;
                    case "imageId":
                    case "positionYMax":
                    case "positionYMin":
                    case "positionXMax":
                    case "positionXMin":
                        String objString = object.toString();
                        int endOfAtom = objString.indexOf('^');

                        entryResult.put(
                                predicateString,
                                objString.substring(0,endOfAtom));
                        break;
                    default:
                        entryResult.put(
                                predicateString,
                                EscapeReasonerResult(object.toString()));
                        break;

                }
            }
            entryResult.put("type", entries);
            result.put(entry.Id, entryResult);
        }
        return  result;
    }

    private String EscapeReasonerResult(String result){
        int indexOfHash = result.lastIndexOf("#");
        // +1 because we want to ignore #
        return  result.substring(indexOfHash +1);
    }
}
