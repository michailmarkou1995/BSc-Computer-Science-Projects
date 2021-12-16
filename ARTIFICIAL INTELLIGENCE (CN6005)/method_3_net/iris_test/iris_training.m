
load iris_data.mat
t = zeros(size(iris_inputs,1),3); 
p = iris_inputs;

% Με το ακόλουθο for loop μετατρέπω τα targets στην απαιτούμενη μορφή:
for i=1:size(t,1) % δηλαδή 150
    if iris_targets_text(i) ==  "Iris-setosa"
        t(i,:) = [1 0 0];
    elseif iris_targets_text(i) ==  "Iris-versicolor"
        t(i,:) = [0 1 0];
    else
        t(i,:) = [0 0 1];  %  αλλιώς Iris-virginica 
    end
end

t = t'; % ανάστροφος γιατί έτσι είναι η μορφή που απαιτεί το train
p = p'; % ανάστροφος γιατί έτσι είναι η μορφή που απαιτεί το train

% Τη γραμμή που ακολουθεί (save) την "τρέχετε" μόνο μια φορά (την πρώτη,  
% όπου δημιουργούνται μέσω του Import Tool του MATLAB οι πίνακες 
% iris_inputs και iris_targets_text ώστε να μη χρειάζεται κάθε φορά
% να χρησιμοποιείτε το Import Tool
%save ('iris_data.mat', 'iris_inputs', 'iris_targets_text');

hidden_layer_neurons = [5]; % αν ήθελα 2 layers με 12 και 8 νευρώνες 
                            % αντίστοιχα σε κάθε layer, θα έγραφα:
                            % hidden_layer_neurons = [12 8]
training_function = 'trainrp'; % default is 'trainscg'. Δείτε το help
                                % του MATLAB (κάντε search "Choose a 
                                % Multilayer Neural Network Training 
                                % Function") για τις υπόλοιπες 9 
                                % παραλλαγές του αλγόριθμου εκπαίδευσης.
net = patternnet(hidden_layer_neurons); % Έτσι δημιουργούμε το νευρωνικό
                                        % μας δίκτυο για classification
                                      
net.divideFcn = 'dividerand'; % default = dividerand (divide randomly)
                              % Υπάρχουν και άλλοι τρόποι, π.χ. divideint, 
                              % divideblock κλπ
net.trainFcn = training_function;
net.trainParam.goal = 1e-4;   % Όταν φτάσει αυτό το error σταματά η 
                              % εκπαίδευση.
net.trainParam.epochs = 1500; % Όταν φτάσει αυτές τις επαναλήψεις σταματά η
                              % εκπαίδευση
net.trainParam.show = 1; % ανά πόσα epochs θα τυπώνει το plot
net.performFcn = 'mse'; % default = crossentropy (το crossentropy είναι 
                        % καλύτερο για classification)

net.divideParam.trainRatio = 70/100; % Η αναλογία των δεδομένων που θα 
                                     % χρησιμοποιηθεί για εκπαίδευση
net.divideParam.valRatio = 0/100; % Αναλογία για testing
net.divideParam.testRatio = 30/100; % Αναλογία για validation

net.trainParam.max_fail = 10;  % by default είναι 6 epochs για το validation 
                               % error (δηλαδή πόσα epochs συνεχίζει να 
                               % αυξάνει το validation error πριν 
                               % σταματήσει το training
% Οι επόμενες δύο γραμμές εφαρμόζονται ΜΟΝΟ αν ο αλγόριθμος εκπαίδευσης 
% είναι ο traingdx. Για περισσότερες πληροφορίες δείτε την ιστοσελίδα:
% https://machinelearningmastery.com/learning-rate-for-deep-learning-neural-networks/
% και το Help του MATLAB
net.trainParam.lr = 0.1;    % learning rate (default=0.01 for traingdx)
net.trainParam.mc = 0.4;    % momentum (default = 0.9 for traingdx)

net = init(net);    % Neural network initialisation (δείτε το Help του MATLAB)

[net,tr] = train(net, p, t);    % (Επιτέλους :-) η εκπαίδευση!)

% ***** MATLAB HELP ***** Δείτε (για εμβάθυνση) τα ακόλουθα στο HELP 
% look for: Classify patterns with a shallow neural network 
% look for: Choose a Multilayer Neural Network Training Function
% look for: Neural Network Object properties
% look for: Improve shallow neural network generalization and avoid
%           overfitting

%view(net);     % Το MATLAB οπτικοποιεί το Νευρωνικό Δίκτυο που φτιάξαμε
outputs = net(p); % για το εκπαιδευμένο ΝΝ , για όλα τα inputs μου
                       % τι output μου δίνει το ΝΝ
figure, plotperform(tr); % οι training καμπύλες (red, green, blue)
figure, plotconfusion(t, outputs);  % έτσι εμφανίζει μόνο το συνολικό 
                                    % confusion matrix. Για τα επιμέρους
                                    % ούτως ή άλλως τα εμφανίζει το 
                                    % training παράθυρο του MATLAB
                                    

