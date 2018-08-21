const replace = require('replace-in-file');
const fs = require('fs');
const path = require('path');

const defaultTensorflowPath= "C:\\tensorflow1\\models\\research\\object_detection";

function walkSync (dir) {
    let filelist = [];
    function readFiles(dir){
        fs.readdirSync(dir).forEach(file => {
            const dirFile = path.join(dir, file);
            try {
                readFiles(dirFile);
            }
            catch (err) {
                if (err.code === 'ENOTDIR' || err.code === 'EBUSY') 
                {
                    if(dirFile.endsWith("xml")){
                        filelist.push(dirFile);
                    }
                }
                else {
                    throw err;
                }
            }
        });
    }
    readFiles(dir);
    
    return filelist;
}

function chagneDirectory(files){
    
    const replaceOptions = {
        //Multiple files
        files: files,  
        //Replacement to make (string or regex) 
        from: defaultTensorflowPath,
        to: objectDetectionPath,
    };

  try {
    const changes = replace.sync(replaceOptions)
    console.log('Modified files:', changes.join(', '));
  }
  catch (error) {
    console.error('Error occurred:', error);
  }
}

console.log(`Current directory: ${__dirname}`);
console.log(`Parent directory: ${path.dirname(__dirname)}`);
const objectDetectionPath = path.join(path.dirname(__dirname),"src", "object_detection");
console.log(`tensorflow directory: ${objectDetectionPath}`);
// console.log(walkSync(objectDetectionPath));
const filesToReplace = walkSync(objectDetectionPath)

chagneDirectory(filesToReplace);